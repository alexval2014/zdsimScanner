using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Globalization;

namespace zdsimScanner
{
    /// <summary>
    /// Перевод Delphi unit NevFunction в C#.
    /// - Много комментариев и заглушек для ReadProcessMemory.
    /// - Предполагает чтение данных из внешнего процесса (по PID).
    /// - Тщательно документировано
    /// </summary>
    public static class NevFunction
    {
        // ------------------------------------------------------------
        // Константы и состояние (эквивалент BaseAddress: Cardinal)
        // ------------------------------------------------------------
        private static IntPtr _processHandle = IntPtr.Zero;
        private static IntPtr _baseAddress = new IntPtr(0x00400000); // аналог BaseAddress
        private static int _processId = 0;

        // Кэш и прочие глобальные переменные
        private static List<string> CachedSpeedData = null;
        private static string CachedRoutePath = string.Empty;
        private static string CachedRouteValue = string.Empty;
        private static string CachedFileName = string.Empty;
        private static uint LastSettingsCheck = 0;

        // Speed ranges storage (как TSpeedRange[])
        private struct SpeedRange
        {
            public int MinRange;
            public int MaxRange;
            public int SpeedLimit;
        }

        private static List<SpeedRange> SpeedRanges = new List<SpeedRange>();
        private static int RangesCount => SpeedRanges.Count;


        // For GetSpeedTargetByTRACK_NextRange caching
        private static int LastTrackValue = -1;
        private static int LastResult = 0;

        // TPiketData
        private struct TPiketData
        {
            public uint Offset;
            public int PiketNum;
            public byte SignalState;
        }


        // ------------------------------------------------------------
        // P/Invoke для ReadProcessMemory
        // ------------------------------------------------------------
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out] byte[] lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            int nSize,
            out IntPtr lpNumberOfBytesWritten);

        // VirtualProtectEx for remote process memory protection change
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool VirtualProtectEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            UIntPtr dwSize,
            uint flNewProtect,
            out uint lpflOldProtect);

        private const int PROCESS_VM_READ = 0x0010;
        private const int PROCESS_QUERY_INFORMATION = 0x0400;
        private const int PROCESS_VM_WRITE = 0x0020;
        private const int PROCESS_VM_OPERATION = 0x0008;
        private const uint PAGE_EXECUTE_READWRITE = 0x40;



        // ------------------------------------------------------------
        // Инициализация / очистка
        // ------------------------------------------------------------
        /// <summary>
        /// Инициализировать чтение по PID внешнего процесса.
        /// </summary>
        public static bool Init(int processId, IntPtr? baseAddress = null)
        {
            try
            {
                // Закрываем старый handle (если был)
                if (_processHandle != IntPtr.Zero)
                {
                    CloseHandle(_processHandle);
                    _processHandle = IntPtr.Zero;
                }

                _processId = processId;
                _processHandle = OpenProcess(PROCESS_VM_READ | PROCESS_QUERY_INFORMATION, false, processId);
                if (_processHandle == IntPtr.Zero) return false;

                if (baseAddress.HasValue)
                    _baseAddress = baseAddress.Value;

                return true;
            }
            catch
            {
                _processHandle = IntPtr.Zero;
                return false;
            }
        }

        /// <summary>
        /// Очистка handle'а
        /// </summary>
        public static void Close()
        {
            if (_processHandle != IntPtr.Zero)
            {
                CloseHandle(_processHandle);
                _processHandle = IntPtr.Zero;
                _processId = 0;
            }
        }

        // ------------------------------------------------------------
        // Низкоуровневые чтения типов (используются в функциях)
        // Все методы безопасно возвращают значение по умолчанию, если чтение не удалось.
        // ------------------------------------------------------------
        private static bool TryReadBytes(IntPtr address, byte[] buffer)
        {
            if (_processHandle == IntPtr.Zero) return false;
            if (address == IntPtr.Zero) return false;

            try
            {
                if (ReadProcessMemory(_processHandle, address, buffer, buffer.Length, out IntPtr read))
                {
                    return read.ToInt32() == buffer.Length || read.ToInt32() > 0;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static int ReadInt32Safe(IntPtr address)
        {
            byte[] b = new byte[4];
            if (!TryReadBytes(address, b)) return 0;
            return BitConverter.ToInt32(b, 0);
        }

        public static byte ReadByteSafe(IntPtr address)
        {
            byte[] b = new byte[1];
            if (!TryReadBytes(address, b)) return 0;
            return b[0];
        }

        public static float ReadFloatSafe(IntPtr address)
        {
            byte[] b = new byte[4];
            if (!TryReadBytes(address, b)) return 0.0f;
            return BitConverter.ToSingle(b, 0);
        }

        public static double ReadDoubleSafe(IntPtr address)
        {
            byte[] b = new byte[8];
            if (!TryReadBytes(address, b)) return 0.0;
            return BitConverter.ToDouble(b, 0);
        }

        // ------------------------------------------------------------
        // Logging (WriteToLog)
        // ------------------------------------------------------------
        public static void WriteToLog(string message)
        {
            try
            {
                string exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string logPath = Path.Combine(exeDir ?? ".", "traffic_lights.log");

                using (var sw = new StreamWriter(logPath, true, Encoding.UTF8))
                {
                    sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - " + message);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    string tmp = Path.GetTempPath();
                    string logPath = Path.Combine(tmp, "traffic_lights.log");
                    using (var sw = new StreamWriter(logPath, true, Encoding.UTF8))
                    {
                        sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - ERROR in WriteToLog: " + ex.Message);
                        sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - " + message);
                    }
                }
                catch
                {
                    // Последнее средство: игнорируем
                }
            }
        }

        // ------------------------------------------------------------
        // Утилитарные функции
        // ------------------------------------------------------------
        public static string BooleanToStr(bool value) => value ? "True" : "False";

        // ------------------------------------------------------------
        public static int Max(int a, int b) => a > b ? a : b;

        // ------------------------------------------------------------
        public static int Min(int a, int b) => a < b ? a : b;

        // ------------------------------------------------------------
        // ReadSettingsValue (парсер settings.ini аналогично Delphi)
        // ------------------------------------------------------------
        public static string ReadSettingsValue(string paramName)
        {
            try
            {
                string exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? ".";
                string settingsPath = Path.Combine(exeDir, "settings.ini");

                if (!File.Exists(settingsPath))
                {
                    WriteToLog("Файл settings.ini не найден: " + settingsPath);
                    return string.Empty;
                }

                foreach (var rawLine in File.ReadAllLines(settingsPath, Encoding.UTF8))
                {
                    var line = rawLine.Trim();
                    if (string.IsNullOrEmpty(line)) continue;
                    if (line.StartsWith(";") || line.StartsWith("#")) continue;

                    int eq = line.IndexOf('=');
                    if (eq <= 0) continue;
                    string key = line.Substring(0, eq).Trim();
                    string value = line.Substring(eq + 1).Trim();

                    if (string.Equals(key, paramName, StringComparison.OrdinalIgnoreCase))
                    {
                        WriteToLog($"{paramName} найден в settings.ini: {value}");
                        return value;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteToLog("Ошибка при чтении settings.ini: " + ex.Message);
            }
            return string.Empty;
        }

        // ------------------------------------------------------------
        // ReadSettingsValue (парсер settings.ini аналогично Delphi)
        // ------------------------------------------------------------
        public static string GetRoutePathFromSettings() => ReadSettingsValue("RoutePath");

        // ------------------------------------------------------------
        // GetCachedSettings (кеширование один раз в секунду)
        // ------------------------------------------------------------
        public static bool GetCachedSettings()
        {
            try
            {
                uint currentTick = (uint)Environment.TickCount;
                if ((currentTick - LastSettingsCheck) < 1000)
                {
                    return !string.IsNullOrEmpty(CachedRoutePath) && !string.IsNullOrEmpty(CachedRouteValue);
                }

                LastSettingsCheck = currentTick;

                string routePath = ReadSettingsValue("RoutePath");
                string routeValue = ReadSettingsValue("Route");
                if (string.IsNullOrEmpty(routeValue)) routeValue = "1";

                if (routePath == CachedRoutePath && routeValue == CachedRouteValue)
                {
                    return true;
                }

                CachedRoutePath = routePath;
                CachedRouteValue = routeValue;
                SpeedRanges.Clear();

                return !string.IsNullOrEmpty(CachedRoutePath);
            }
            catch
            {
                return false;
            }
        }

        // ------------------------------------------------------------
        // LoadSpeedRanges (перевод LoadSpeedRanges с Delphi)
        // ------------------------------------------------------------
        public static void LoadSpeedRanges()
        {
            /*
            if (!GetCachedSettings()) return;

            string speedFileName = CachedRouteValue == "2" ? "speeds2.dat" : "speeds1.dat";
            if (string.Equals(speedFileName, CachedFileName, StringComparison.OrdinalIgnoreCase)) return;

            CachedFileName = speedFileName;
            SpeedRanges.Clear();

            string exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? ".";
            string speedFilePath = Path.Combine(exeDir, "routes", CachedRoutePath, speedFileName);

            if (!File.Exists(speedFilePath)) return;

            try
            {
                var lines = File.ReadAllLines(speedFilePath, Encoding.UTF8);
                // Предварительный capacity
                SpeedRanges.Capacity = Math.Max(200, lines.Length);

                foreach (var raw in lines)
                {
                    var line = raw.Trim();
                    if (string.IsNullOrEmpty(line)) continue;

                    // Попробуем сначала табы, затем пробелы
                    string[] parts = line.Split('\t', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 3)parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length >= 3)
                    {
                        if (int.TryParse(parts[0].Trim(), out int start) &&
                            int.TryParse(parts[1].Trim(), out int end) &&
                            int.TryParse(parts[2].Trim(), out int limit))
                        {
                            SpeedRange r;
                            if (start <= end)
                            {
                                r.MinRange = start;
                                r.MaxRange = end;
                            }
                            else
                            {
                                r.MinRange = end;
                                r.MaxRange = start;
                            }
                            r.SpeedLimit = limit;
                            SpeedRanges.Add(r);
                        }
                        else
                        {
                            WriteToLog("Ошибка парсинга строки speeds: " + line);
                        }
                    }
                }

                // Сортировка по MinRange (быстрая встроенная сортировка)
                SpeedRanges.Sort((a, b) => a.MinRange.CompareTo(b.MinRange));

                WriteToLog($"LoadSpeedRanges: загружено {SpeedRanges.Count} диапазонов из {speedFileName}");
            }
            catch (Exception ex)
            {
                WriteToLog("Ошибка LoadSpeedRanges: " + ex.Message);
            }
            */
        }

        // ------------------------------------------------------------
        // ConvertToDistance и обёртки
        // ------------------------------------------------------------
        public static string ConvertToDistance(int value, int addMeters = 1101)
        {
            try
            {
                int metersInKilometer = 1000;
                int metersInPk = 100;
                int totalMeters = value + addMeters;
                int kilometers = totalMeters / metersInKilometer;
                int remainingMeters = totalMeters % metersInKilometer;
                int pikets = remainingMeters / metersInPk;
                int meters = remainingMeters % metersInPk;

                return $"{kilometers}км{pikets}пк{meters}м";
            }
            catch
            {
                return "0км 0пк0м";
            }
        }

        // ------------------------------------------------------------
        // ConvertToDistance и обёртки
        // ------------------------------------------------------------
        public static string ConvertToDistanceFromString(string value, int addMeters = 1101)
        {
            if (int.TryParse(value, out int v))
                return ConvertToDistance(v, addMeters);
            else
                return "0км 0пк0м";
        }

        // ------------------------------------------------------------
        // GetCoordinatesFormatted (чтение double -> округление -> ConvertToDistance)
        // ------------------------------------------------------------
        public static string GetCoordinatesFormatted()
        {
            try
            {
                // тело Delphi: val := PDouble(BaseAddress + $403F50)^;
                IntPtr addr = IntPtr.Add(_baseAddress, 0x403F50);
                double val = ReadDoubleSafe(addr);
                long intVal = (long)Math.Round(Math.Abs(val));
                return ConvertToDistance((int)intVal);
            }
            catch
            {
                return "0км 0пк0м";
            }
        }

        // ------------------------------------------------------------
        // GetAcceleration
        // ------------------------------------------------------------
        public static string GetAcceleration()
        {
            try
            {
                IntPtr addr = IntPtr.Add(_baseAddress, 0x3498B8);
                double val = ReadDoubleSafe(addr);
                return val.ToString("0.00", CultureInfo.InvariantCulture);
            }
            catch
            {
                return "Err";
            }
        }

        // ------------------------------------------------------------
        // GetTrackNumber / GetTrackNumberInt
        // ------------------------------------------------------------
        public static string GetTrackNumber()
        {
            try
            {
                IntPtr addr = IntPtr.Add(_baseAddress, 0x4F8D958);
                int raw = ReadInt32Safe(addr);
                byte val = (byte)(raw & 0xFF);
                return val.ToString();
            }
            catch
            {
                return "Err";
            }
        }

        public static byte GetTrackNumberInt()
        {
            try
            {
                IntPtr addr = IntPtr.Add(_baseAddress, 0x4F8D958);
                return ReadByteSafe(addr);
            }
            catch
            {
                return 0;
            }
        }

        // ------------------------------------------------------------
        // GetALS (PByte(Base + $8C07ECC)^)
        // ------------------------------------------------------------
        public static byte GetALS()
        {
            try
            {
                IntPtr addr = IntPtr.Add(_baseAddress, 0x8C07ECC);
                return ReadByteSafe(addr);
            }
            catch
            {
                return 0;
            }
        }

        // ------------------------------------------------------------
        // Pressure TС: GetPressureTCf (сложная функция с ветвлением по типу локомотива)
        // Перевод твоей Delphi реализации.
        // ------------------------------------------------------------
        private static uint GetPtrAddr(uint baseOffset, uint[] offsets)
        {
            try
            {
                // читаем 32-bit значение по baseAddr + baseOffset
                IntPtr readPtrAddr = IntPtr.Add(_baseAddress, (int)baseOffset);
                int first = ReadInt32Safe(readPtrAddr);
                uint currentAddr = (uint)first;
                if (currentAddr == 0) return 0;

                // Внимание: ты говорил "просто прибавляем смещения, НЕ читаем по ним указатели"
                // Поэтому реализуем именно сумму (как в твоём Delphi коде)
                foreach (var off in offsets)
                {
                    currentAddr = currentAddr + off;
                }

                return currentAddr;
            }
            catch
            {
                return 0;
            }
        }

        public static float GetPressureTCf()
        {
            float result = 0.0f;
            try
            {
                // Аналог baseAddr := $00400000;
                // читаем locType := PInteger(Pointer(baseAddr + $4F8D93C))^;
                IntPtr locTypeAddr = IntPtr.Add(_baseAddress, 0x4F8D93C);
                int locType = ReadInt32Safe(locTypeAddr);

                switch (locType)
                {
                    // Группа 1: ED9M, ED4M, 2M62 - используем float
                    case 3159:
                    case 3154:
                    case 1462:
                        {
                            IntPtr addr = IntPtr.Add(_baseAddress, 0x8D107B0);
                            result = ReadFloatSafe(addr);
                        }
                        break;

                    // Группа 2: double
                    case 21014:
                    case 2071:
                    case 2070:
                    case 201318:
                    case 885:
                    case 880:
                    case 882:
                        {
                            uint addru = GetPtrAddr(0x4F8D8D4, new uint[] { 0xA8 });
                            if (addru != 0)
                                result = (float)ReadDoubleSafe(new IntPtr((long)addru));
                            else
                                result = 0.0f;
                        }
                        break;

                    // Группа 3: float via pointer
                    case 811:
                    case 343:
                    case 31714:
                    case 23152:
                        {
                            uint addru = GetPtrAddr(0x8D10D78, new uint[] { 0x68 });
                            if (addru != 0)
                                result = ReadFloatSafe(new IntPtr((long)addru));
                            else
                                result = 0.0f;
                        }
                        break;

                    // Группа 4: CHS7 special case
                    case 822:
                        {
                            IntPtr tmpAddr = IntPtr.Add(_baseAddress, 0x4F8D8D4);
                            int tempAddr = ReadInt32Safe(tmpAddr);
                            if (tempAddr != 0)
                            {
                                long full = (long)tempAddr + 0x80;
                                result = (float)ReadDoubleSafe(new IntPtr(full));
                            }
                            else
                                result = 0.0f;
                        }
                        break;

                    case 621:
                        {
                            uint addru = GetPtrAddr(0x4F8D8D4, new uint[] { 0x58 });
                            if (addru != 0)
                                result = (float)ReadDoubleSafe(new IntPtr((long)addru));
                            else
                                result = 0.0f;
                        }
                        break;

                    case 523:
                        result = 0.0f;
                        break;

                    case 524:
                        {
                            uint addru = GetPtrAddr(0x34846C, new uint[] { 0x1C, 0x80 });
                            if (addru != 0)
                                result = (float)ReadDoubleSafe(new IntPtr((long)addru));
                            else
                                result = 0.0f;
                        }
                        break;

                    case 812:
                    case 23142:
                        {
                            uint addru = GetPtrAddr(0x04F8D8D4, new uint[] { 0x28, 0x48 });
                            if (addru != 0)
                                result = ReadFloatSafe(new IntPtr((long)addru));
                            else
                                result = 0.0f;
                        }
                        break;

                    default:
                        {
                            uint addru = GetPtrAddr(0x04F8D8D4, new uint[] { 0x28, 0x48 });
                            if (addru != 0)
                                result = ReadFloatSafe(new IntPtr((long)addru));
                            else
                                result = 0.0f;
                        }
                        break;
                }

                // Проверяем разумность значения (0..15)
                if (result < 0.0f || result > 15.0f)
                    result = 0.0f;
            }
            catch
            {
                result = 0.0f;
            }

            return result;
        }

        // Строковая обёртка для давления ТЦ (GetPressureTC)
        public static string GetPressureTC()
        {
            try
            {
                float val = GetPressureTCf();
                return val.ToString("0.00", CultureInfo.InvariantCulture);
            }
            catch
            {
                return "Err";
            }
        }

        // ------------------------------------------------------------
        // Здесь — заглушки / места для реализации: функции, которые ты
        // объявил в interface Delphi, но не предоставил реализацию.
        // Их можно заполнить по мере присылки соответствующих Delphi-фрагментов.
        // ------------------------------------------------------------

        // Пример: GetTrafficLightsSequence — ты посылал Delphi-версию, но
        // полную логику (ReadTrafficLightData и т.п.) я не видел окончательно.
        public static string GetTrafficLightsSequence()
        {
            // TODO: реализовать полную логику согласно твоим файлам svetofor1.dat / svetofor2.dat
            return "NotImplemented_GetTrafficLightsSequence";
        }

        public static string GetSpeed() => "NotImplemented_GetSpeed";
        public static string GetDistance() => "NotImplemented_GetDistance";
        public static string GetCurrentDate() => DateTime.Now.ToString("yyyy-MM-dd");
        public static string GetCurrentTime() => DateTime.Now.ToString("HH:mm:ss");
        public static string GetLimitSpeed() => "NotImplemented_GetLimitSpeed";
        public static int GetLimitSpeedValue() => 0;
        public static float GetSpeedValue2() => 0.0f;
        public static string GetCurrentStation() => "NotImplemented_GetCurrentStation";
        public static string GetChannel() => "NotImplemented_GetChannel";
        public static string GetTrackWithDirection() => "NotImplemented_GetTrackWithDirection";
        public static string GetTargetType() => "NotImplemented_GetTargetType";

        public static string GetSvetoforValue() => "NotImplemented_GetSvetoforValue";
        public static bool GetDirection() => true;
        public static string GetSvetoforFileName() => "NotImplemented_GetSvetoforFileName";
        public static string GetRoutePathFromMemory() => "NotImplemented_GetRoutePathFromMemory";

        public static int GetSpeedValue() => 0;
        public static int GetTargetSpeedValue() => 0;
        public static int GetDistanceValue() => 0;

        public static byte GetALSWrapper() => GetALS(); // уже реализована GetALS()

        public static float GetPressureTMf() => 0.0f;
        public static float GetPressureURf() => 0.0f;

        public static int GetSpeedLimitByTRACK() => 0;
        public static int GetSpeedTargetByTRACK_NextRange() => 0;

        // ------------------------------------------------------------
        // Утилиты / тесты
        // ------------------------------------------------------------
        /// <summary>
        /// Быстрый debug-дамп: прочитать 8 байт по адресу и вывести hex
        /// </summary>
        public static string DumpBytes(IntPtr addr, int count = 8)
        {
            try
            {
                byte[] buf = new byte[count];
                if (!TryReadBytes(addr, buf)) return "(no data)";
                var sb = new StringBuilder();
                foreach (var b in buf) sb.AppendFormat("{0:X2} ", b);
                return sb.ToString().Trim();
            }
            catch (Exception ex)
            {
                return "Err: " + ex.Message;
            }
        }
    }
}

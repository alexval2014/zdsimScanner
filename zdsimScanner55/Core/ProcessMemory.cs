using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using zdsimScanner.Models;
using zdsimScanner.NativeApi;

namespace zdsimScanner.Core
{
    /// <summary>
    /// Обёртка по работе с процессом и памятью. 
    /// Перенесён из большого Loco.cs для разделения ответственности.
    /// </summary>
    public static class ProcessMemory
    {
        // константы доступа (как у тебя)
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_READ = 0x0010;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;

        public static int P_ID;
        public static int P_HANDLE; // держим int чтобы минимально менять остальной код
        public static Process CurrentProcess;
        public static IntPtr BA; // базовый адрес Launcher.exe
        public static IntPtr BA_openal32;

        /// <summary>Открывает процесс по имени (возвращает true если успешно)</summary>
        public static bool OpenProcessByName(string p_name)
        {
            Process[] p = Process.GetProcessesByName(p_name);
            if (p.Length == 0) return false;
            CurrentProcess = p[0];
            var h = NativeMethods.OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, CurrentProcess.Id);
            if ((int)h != 0)
            {
                P_ID = CurrentProcess.Id;
                P_HANDLE = (int)h;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Читает произвольное количество байт из абсолютного адреса
        /// </summary>
        public static byte[] ReadBytes(Int32 addr, int size)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[size];

            NativeMethods.ReadProcessMemory(
                Loco.P_HANDLE,                     // IntPtr
                new IntPtr(addr),                  // преобразуем int → IntPtr
                buffer,
                size,
                ref bytesRead);

            return buffer;
        }


        /// <summary>Записать массив байт в абсолютный адрес</summary>
        public static void WriteBytes(Int32 addr, byte[] buffer)
        {
            int bytesWritten = 0;

            NativeMethods.WriteProcessMemory(
                Loco.P_HANDLE,                 // дескриптор процесса (IntPtr)
                new IntPtr(addr),              // преобразуем int → IntPtr
                buffer,                        // данные для записи
                buffer.Length,                 // размер данных
                ref bytesWritten);             // сколько реально записано
        }


        /// <summary>
        /// Читает указатель: берет значение по baze_address (4 байта) и проходит offsets.
        /// Возвращает финальный абсолютный адрес (как у тебя read_pointer).
        /// </summary>
        public static int ReadPointer(Int32 baze_address, int[] offsets)
        {
            baze_address = BitConverter.ToInt32(ReadBytes(baze_address, 4), 0);
            if (baze_address == 0) return 0;

            for (int i = 0; i < offsets.Length; i++)
            {
                baze_address += offsets[i];

                if (i == offsets.Length - 1)
                {
                    return baze_address;
                }

                if (baze_address == 0)
                {
                    // Помечаем внешний флаг (можно заменить на событие/исключение позже)
                    // оставим как у тебя: Loco.i_baze_address_flag_fail = 1;
                    return 0;
                }
                else
                {
                    baze_address = BitConverter.ToInt32(ReadBytes(baze_address, 4), 0);
                }
            }
            return baze_address;
        }

        //------------------------------------------------------------------------------------
        // словарь сигнатур локомотивов
        private static readonly Dictionary<int, int> locoSignatures = new Dictionary<int, int>
        {
             { 23152,  1 },  // 2ES5K
             { 31714,  2 },  // EP1m
             { 343,    3 },  // CHS2K
             { 523,    4 },  // CHS4
             { 524,    5 },  // CHS4 KVR
             { 621,    6 },  // CHS4t
             { 822,    7 },  // CHS7
             { 812,    8 },  // CHS8
             { 811,    9 },  // VL11m
             { 882,   10 },  // VL82m
             { 880,   11 },  // VL80t
             { 885,   12 },  // VL85
             { 2070,  13 },  // TEP70
             { 21014, 14 },  // 2TE10U
             { 1462,  15 },  // M62
             { 3154,  16 },  // ED4M
             { 3159,  17 }, // ED9M
             { 201318,18 }  // TEM18dm
        };

        // словарь для отображения названий по sig_loco
        private static readonly Dictionary<int, string> locoNames = new Dictionary<int, string>
        {
             { 1, "2ES5K" },
             { 2, "EP1m" },
             { 3, "CHS2K" },
             { 4, "CHS4" },
             { 5, "CHS4 KVR" },
             { 6, "CHS4t" },
             { 7, "CHS7" },
             { 8, "CHS8" },
             { 9, "VL11m" },
             { 10, "VL82m" },
             { 11, "VL80t" },
             { 12, "VL85" },
             { 13, "TEP70" },
             { 14, "2TE10U" },
             { 15, "M62" },
             { 16, "ED4M" },
             { 17, "ED9M" },
             { 18, "TEM18dm" }
        };

        //-----------------------------------------------------------------------------------
        //ищем локомотив в процессе Launcher.exe
        //-----------------------------------------------------------------------------------
        public static int find_loco()
        {
            //Получаем minimum и maximum адресс
            NativeMethods.SYSTEM_INFO sys_info = new NativeMethods.SYSTEM_INFO();
            NativeMethods.GetSystemInfo(out sys_info);

            // Список модулей процесса
            List<ProcessModule> m_module = new List<ProcessModule>();

            int i_name = 0;
            for (int i = 0; i < Loco.current_process.Modules.Count; i++)
            {
                m_module.Add(Loco.current_process.Modules[i]);

                if (Convert.ToString(m_module[i].ModuleName) == "Launcher.exe")
                {
                    BA = m_module[i].BaseAddress;
                    i_name = 2;
                }

                if (Convert.ToString(m_module[i].ModuleName) == "OpenAL32.dll")
                {
                    BA_openal32 = m_module[i].BaseAddress;
                }
            }

            // Получаем базовый адрес модуля
            IntPtr proc_min_address = sys_info.minimumApplicationAddress;
            IntPtr proc_max_address = sys_info.maximumApplicationAddress;

            //сохранение значений в виде длинных целых чисел, так что мне не придется делать много приведений позже
            long proc_min_address_l = (long)proc_min_address;
            long proc_max_address_l = (long)proc_max_address;

            if (i_name == 2)
            {
                Process process = Process.GetProcessesByName("launcher")[0];

                //открытие процесса с желаемым уровнем доступа
                IntPtr processHandle = NativeMethods.OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, process.Id);
            }

            Loco.sig_loco = 0;

            //Читаем из памяти процеса игры версию ZD simulator
            LocoMemoryHelpers.temp_buffer = ReadBytes((int)BA + 0x0000011d, 1);


            //4F711B4

            /*
            // ---------------------- V54.006 --------------------------
            if (LocoMemoryHelpers.temp_buffer[0] == 0x9c)
            {
                Loco.i_process_name = 6; // версия 54.006

                //читаем путь zdsim
                int i_flag_string = 0;
                for (int i = 0; i_flag_string < 2; i++)
                {
                    LocoMemoryHelpers.temp_buffer = ReadBytes((Int32)BA_openal32 + 0x00dacfc + i, 1);
                    if (LocoMemoryHelpers.temp_buffer[0] != 0)
                    {
                        i_flag_string = 0;
                        LocoMemoryHelpers.temp_buffer2[i] = LocoMemoryHelpers.temp_buffer[0];
                    }
                    else
                    {
                        i_flag_string++;
                    }

                    LocoMemoryHelpers.temp_buffer3 = new byte[i];
                    Array.Copy(LocoMemoryHelpers.temp_buffer2, 0, LocoMemoryHelpers.temp_buffer3, 0, i);
                }

                Loco.s_path_zdsim_sound = Encoding.ASCII.GetString(LocoMemoryHelpers.temp_buffer3);
                Loco.s_path_zdsim_sound = Loco.s_path_zdsim_sound.Remove(Loco.s_path_zdsim_sound.LastIndexOf(@"\Lau") + 1);
                Loco.s_path_zdsim_sound = Loco.s_path_zdsim_sound + "sound";

                Loco.locoCode = BitConverter.ToInt32(ReadBytes((int)BA + 0x4F711B4, 4), 0);
                Console.WriteLine($"[DEBUG] V54.006 locoCode = {Loco.locoCode} (0x{Loco.locoCode:X})");


                if (locoSignatures.TryGetValue(Loco.locoCode, out int locoId))
                {
                    //Loco.sig_loco = locoId;
                    Console.WriteLine($"[DEBUG] Локомотив найден: sig_loco = {Loco.sig_loco}");
                }
                else
                {
                    Console.WriteLine("[DEBUG] Локомотив в словаре не найден!");
                }
            }
            */

            // ---------------------- V54.006 --------------------------
            if (LocoMemoryHelpers.temp_buffer[0] == 0x9c)
            {
                Loco.i_process_name = 6; // версия 54.006
                Loco.locoCode = BitConverter.ToInt32(ReadBytes((int)BA + 0x04F711B4, 4), 0);

                Console.WriteLine($"[DEBUG] V54.006 locoCode = {Loco.locoCode} (0x{Loco.locoCode:X})");

                if (LocoSignatures.Signatures.TryGetValue(Loco.locoCode, out LocoType locoId))
                {
                    Loco.sig_loco2 = locoId;
                    Console.WriteLine($"[DEBUG] Локомотив найден: sig_loco = {Loco.sig_loco}");
                }
                else
                {
                    Loco.sig_loco2 = LocoType.Unknown;
                    Console.WriteLine("[DEBUG] Локомотив в словаре не найден!");
                }
            }

            // ---------------------- V55.008 --------------------------
            if (LocoMemoryHelpers.temp_buffer[0] == 0x68)
            {
                Loco.i_process_name = 7; // версия 55.008
                Loco.locoCode = BitConverter.ToInt32(ReadBytes((int)BA + 0x04F8D93C, 4), 0);

                Console.WriteLine($"[DEBUG] V55.008 locoCode = {Loco.locoCode} (0x{Loco.locoCode:X})");

                if (LocoSignatures.Signatures.TryGetValue(Loco.locoCode, out LocoType locoId))
                {
                    Loco.sig_loco2 = locoId;
                    Console.WriteLine($"[DEBUG] Локомотив найден: sig_loco = {Loco.sig_loco}");
                }
                else
                {
                    Loco.sig_loco2 = LocoType.Unknown;
                    Console.WriteLine("[DEBUG] Локомотив в словаре не найден!");
                }
            }

            Console.WriteLine($"[DEBUG] BaseAddress launcher.exe = 0x{BA.ToInt64():X}");
            LocoMemoryHelpers.InitBuffers();
            return Loco.sig_loco;
        }

        //return (int)Loco.sig_loco;
        //-----------------------------------------------------------------------------------
        // Ищем указатель пневматики
        //-----------------------------------------------------------------------------------
        public static int find_pnevm()
        {
            Console.WriteLine("Поиск пневматики...");

            //статические ZD simulator v54.006
            if (Loco.i_process_name == 6)
            {
                LocoMemoryHelpers.temp_buffer = ReadBytes((Int32)BA + 0x04F7114C, 4);
            }

            //статические ZD simulator v55.008
            if (Loco.i_process_name == 7)
            {
                LocoMemoryHelpers.temp_buffer = ReadBytes((Int32)BA + 0x4F8D8D4, 4);
            }

            Loco.sig_pos_pnevm = BitConverter.ToInt32(LocoMemoryHelpers.temp_buffer, 0);
            Console.WriteLine(Loco.sig_pos_pnevm);
            return Loco.sig_pos_pnevm;
        }

        //-----------------------------------------------------------------------------------
        // Ищем указатель электрики
        //-----------------------------------------------------------------------------------
        public static int find_elektro()
        {
            //Console.WriteLine("Поиск электрики...");
            //статические ZD simulator v54.006
            if (Loco.i_process_name == 6)
            {
                LocoMemoryHelpers.temp_buffer = ReadBytes((Int32)BA + 0x062531B4, 4);
            }

            Loco.sig_pos_elektro = BitConverter.ToInt32(LocoMemoryHelpers.temp_buffer, 0);
            return Loco.sig_pos_elektro;
        }
    }
}
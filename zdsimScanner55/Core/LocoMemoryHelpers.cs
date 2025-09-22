using System;
using System.Drawing;
using System.Threading;
using zdsimScanner.Core;

namespace zdsimScanner.Core
{
    /// <summary>
    /// Хелперы для чтения/записи значений локомотива в буфер out_buffer.
    /// Используют ProcessMemory.ReadBytes/WriteBytes.
    /// </summary>
    public static class LocoMemoryHelpers
    {
        // флаг отладки
        public static bool DebugMode = true; // ставь false чтобы отключить лог

        // Общие буферы (раньше были в Loco.cs)
        public static byte[] out_buffer = new byte[64];
        public static byte[] in_buffer = new byte[64];
        public static byte[] temp_buffer = new byte[128];
        public static byte[] temp_buffer2 = new byte[128];
        public static byte[] temp_buffer3 = new byte[128];
        public static byte[] out_buffer1 = new byte[128];
        public static byte[] temp_buffer4 = new byte[128];


        public static void InitBuffers()
        {
            out_buffer = new byte[64];
            in_buffer = new byte[64];
            temp_buffer = new byte[128];
            temp_buffer2 = new byte[128];
            temp_buffer3 = new byte[128];
        }

        // ===============================================================
        // Чтение 1 байта в out_buffer (offset — относительный к BA)
        // ReadByteToBuffer(Loco_2ES5K.Lamp_TMLeak, outIndex: 20);
        // ===============================================================
        public static void ReadByteToBuffer(int offset, int outIndex, byte[] cache = null)
        {
            byte[] buf = ProcessMemory.ReadBytes((Int32)ProcessMemory.BA + offset, 1);
            byte val = (buf != null && buf.Length > 0) ? buf[0] : (byte)0;
            out_buffer[outIndex] = val;
            if (cache != null) cache[0] = val;
        }

        // ===============================================================
        // Чтение 2 байт (UInt16) напрямую
        // ===============================================================
        public static void ReadUInt16ToBuffer(int offset, int outIndex)
        {
            byte[] buf = ProcessMemory.ReadBytes((Int32)ProcessMemory.BA + offset, 2);
            Array.Copy(buf, 0, out_buffer, outIndex, 2);
        }


        // ===============================================================
        // Чтение 2 байт (UInt16) напрямую в переменную Form1.i_skor_dop
        // ===============================================================
        public static void ReadUInt16ToVar(int offset)
        {
            try
            {
                // читаем 2 байта из памяти по адресу (BA + offset)
                byte[] buf = ProcessMemory.ReadBytes((Int32)ProcessMemory.BA + offset, 2);

                // преобразуем в UInt16
                UInt16 val = BitConverter.ToUInt16(buf, 0);

                // сохраняем в переменную (автоматически расширяется до int)
                Form1.i_skor_dop = val;

                #if DEBUG
                Console.WriteLine($"[DEBUG] i_skor_dop = {Form1.i_skor_dop} (offset=0x{offset:X})");
                #endif
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] ReadUInt16ToVar(offset=0x{offset:X}): {ex.Message}");
                Form1.i_skor_dop = 0; // в случае ошибки сбрасываем
            }
        }

        // ===============================================================
        // функцию: читает float по адресу; берёт модуль значения;
        // умножает на коэффициент(scale или Loco.i_skor_tek_convert); 
        // преобразует в UInt16;пишет в out_buffer.
        // Если нужен фиксированный коэффициент (например, scale: 10f),
        // просто передаёшь factor: 10f.
        // Если нужна настройка через переменную — используешь Loco.i_skor_tek_convert.
        // Чтение float (4 байта) → масштабирование → UInt16 → запись 2 байт
        // ===============================================================
        public static void ReadScaledFloatToBufferWithFactor(int offset, int outIndex, float factor)
        {
            byte[] buf = ProcessMemory.ReadBytes((Int32)ProcessMemory.BA + offset, 4);
            float f = BitConverter.ToSingle(buf, 0);
            f = Math.Abs(f);

            // масштабирование
            float scaled = f * factor;

            // ограничение в диапазоне ushort
            if (scaled < UInt16.MinValue) scaled = UInt16.MinValue;
            if (scaled > UInt16.MaxValue) scaled = UInt16.MaxValue;

            UInt16 val = (UInt16)scaled;

            byte[] bval = BitConverter.GetBytes(val);
            Array.Copy(bval, 0, out_buffer, outIndex, 2);
        }

        public static void ReadScaledDoubleToBufferAbsolute(int absAddress, int outIndex, double factor, bool forceDebug = false)
        {
            bool dbg = DebugMode || forceDebug;

            if (absAddress == 0)
            {
                if (dbg) Console.WriteLine($"[DBG] ReadScaledDoubleToBufferAbsolute: absAddress == 0, outIndex={outIndex}");
                return;
            }

            if (dbg) Console.WriteLine($"[DBG] ReadScaledDoubleToBufferAbsolute: reading 8 bytes at 0x{absAddress:X} (outIndex={outIndex}, factor={factor})");

            byte[] buf = null;
            try
            {
                buf = ProcessMemory.ReadBytes(absAddress, 8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DBG] ReadScaledDoubleToBufferAbsolute: ReadBytes threw: {ex.Message}");
                return;
            }

            if (buf == null)
            {
                Console.WriteLine($"[DBG] ReadScaledDoubleToBufferAbsolute: ReadBytes returned null at 0x{absAddress:X}");
                return;
            }

            if (buf.Length < 8)
            {
                Console.WriteLine($"[DBG] ReadScaledDoubleToBufferAbsolute: ReadBytes returned {buf.Length} bytes (expected 8) at 0x{absAddress:X} -> {BitConverter.ToString(buf)}");
                return;
            }

            // raw hex
            string rawHex = BitConverter.ToString(buf);

            double value;
            try
            {
                value = BitConverter.ToDouble(buf, 0); // little-endian on Windows
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DBG] BitConverter.ToDouble failed at 0x{absAddress:X}: {ex.Message}; raw={rawHex}");
                return;
            }

            double absValue = Math.Abs(value);
            double scaled = absValue * factor;

            // clamp to UInt16 range
            if (scaled < UInt16.MinValue) scaled = UInt16.MinValue;
            if (scaled > UInt16.MaxValue) scaled = UInt16.MaxValue;

            UInt16 finalVal = (UInt16)Math.Round(scaled); // округляем для стабильности
            byte[] outBytes = BitConverter.GetBytes(finalVal);

            // safety: ensure out_buffer large enough
            if (outIndex < 0 || outIndex + 1 >= out_buffer.Length)
            {
                Console.WriteLine($"[DBG] Out of range target index: outIndex={outIndex}, out_buffer.Length={out_buffer.Length}");
                return;
            }

            Array.Copy(outBytes, 0, out_buffer, outIndex, 2);

            if (dbg)
            {
                Console.WriteLine(
                    $"[DBG] 0x{absAddress:X}: raw={rawHex} -> double={value} abs={absValue} *{factor} => scaled={scaled} clamped={finalVal} bytes={BitConverter.ToString(outBytes)} wrote@{outIndex}");
            }
        }

        /// <summary>
        /// Читает давление (double) из памяти, масштабирует и пишет в out_buffer.
        /// Внутри сама прибавляет sig_pos_pnevm.
        /// </summary>
        /// <param name="offset">Смещение из таблицы офсетов (например, Loco_2ES5K.PNM)</param>
        /// <param name="outIndex">Смещение в out_buffer, куда писать 2 байта</param>
        public static void ReadPressureToBuffer(int offset, int outIndex, double scale)
        {
            try
            {
                // адрес считается автоматически
                int addr = Loco.sig_pos_pnevm + offset;

                // читаем 8 байт (double)
                byte[] buf = ProcessMemory.ReadBytes(addr, 8);
                double dVal = BitConverter.ToDouble(buf, 0);

                // защита от NaN и переполнений
                if (double.IsNaN(dVal) || double.IsInfinity(dVal)) dVal = 0;
                dVal = Math.Abs(dVal);
                UInt16 iVal;
                try
                {
                    iVal = Convert.ToUInt16(dVal * scale);
                }
                catch (OverflowException)
                {
                    iVal = UInt16.MaxValue; // защита от выхода за диапазон
                }

                byte[] bval = BitConverter.GetBytes(iVal);
                Array.Copy(bval, 0, out_buffer, outIndex, 2);

                // вывод отладки
                Console.WriteLine($"[DEBUG] Pressure offset=0x{offset:X}, addr=0x{addr:X}, raw={dVal:F2}, scaled={iVal}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] ReadPressureToBuffer(offset=0x{offset:X}): {ex.Message}");
            }
        }

        public static void ReadScaledDoubleToBufferWithFactor(int offset, int outIndex, double factor)
        {
            byte[] buf = ProcessMemory.ReadBytes((Int32)ProcessMemory.BA + offset, 8);
            double d = BitConverter.ToDouble(buf, 0);
            d = Math.Abs(d);

            double scaled = d * factor;
            if (scaled < UInt16.MinValue) scaled = UInt16.MinValue;
            if (scaled > UInt16.MaxValue) scaled = UInt16.MaxValue;

            UInt16 val = (UInt16)scaled;

            byte[] bval = BitConverter.GetBytes(val);
            Array.Copy(bval, 0, out_buffer, outIndex, 2);
        }

        public static void ReadScaledFloatToBufferWithFactor1(int offset, int outIndex, float factor) 
        {   
            byte[] buf = ProcessMemory.ReadBytes((Int32)ProcessMemory.BA + offset, 4); 
            float f = BitConverter.ToSingle(buf, 0); 
            f = Math.Abs(f);
            // UInt16 val = Convert.ToUInt16(f * factor); 
            UInt16 val = Convert.ToUInt16(f);
            byte[] bval = BitConverter.GetBytes(val); 

            Array.Copy(bval, 0, out_buffer, outIndex, 2); 
        }

        // ===============================================================
        // Чтение double (8 байт) → масштабирование → UInt16 → запись 2 байт
        // ===============================================================
      
        
        /*
         public static void ReadScaledDoubleToBuffer(int offset, int outIndex, double scale)
        {
            byte[] buf = ProcessMemory.ReadBytes((Int32)ProcessMemory.BA + offset, 8);
            double d = BitConverter.ToDouble(buf, 0);
            d = Math.Abs(d);
            UInt16 val = Convert.ToUInt16(d * scale);
            byte[] bval = BitConverter.GetBytes(val);
#if DEBUG
            Console.WriteLine($"[DEBUG] napruga KS = {val} (offset=0x{offset:X})");
#endif
            Array.Copy(bval, 0, out_buffer, outIndex, 2);
        }
        */

        /*
           попробовать как абсолютный адрес:
           LocoMemoryHelpers.DebugReadDoubleAt(0x08CF3F30, true);
           попробовать как offset (BA + offset):
           LocoMemoryHelpers.DebugReadDoubleAt(0x08CF3F30, false);
           Если при isAbsolute=true ты видишь в лог корректное double ~4.2, 
           а при false — нет, значит у тебя в константе лежит абсолютный адрес, 
           а не смещение. Тогда нужно из абсолютного адреса вычислить смещение (абсолютный - BA).
         */


        public static void DebugReadDoubleAt(int addrOrOffset, bool isAbsolute)
        {
            try
            {
                int addr = isAbsolute ? addrOrOffset : (ProcessMemory.BA == IntPtr.Zero ? 0 : ProcessMemory.BA.ToInt32() + addrOrOffset);
                Console.WriteLine($"[DBG] DebugReadDoubleAt: isAbsolute={isAbsolute}, param=0x{addrOrOffset:X}, computedAddr=0x{addr:X}");
                if (addr == 0)
                {
                    Console.WriteLine("[DBG] computed address == 0 (BA==0 ?)");
                    return;
                }

                var buf = ProcessMemory.ReadBytes(addr, 8);
                if (buf == null)
                {
                    Console.WriteLine($"[DBG] ReadBytes returned null for 0x{addr:X}");
                    return;
                }
                if (buf.Length < 8)
                {
                    Console.WriteLine($"[DBG] ReadBytes returned {buf.Length} bytes for 0x{addr:X}: {BitConverter.ToString(buf)}");
                    return;
                }

                Console.WriteLine($"[DBG] RAW @0x{addr:X}: {BitConverter.ToString(buf)}");
                double d = BitConverter.ToDouble(buf, 0);
                float f = BitConverter.ToSingle(buf, 0);
                ulong u64 = BitConverter.ToUInt64(buf, 0);
                uint u32 = BitConverter.ToUInt32(buf, 0);

                Console.WriteLine($"[DBG] Interpretations -> double={d}, float={f}, u64=0x{u64:X}, u32=0x{u32:X}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DBG] Exception DebugReadDoubleAt: {ex.Message}");
            }
        }

        //=============================================================================
        public static void ReadScaledDoubleToBuffer(int addrOrOffset, bool isAbsolute, int outIndex, double scale)
        {
            try
            {
                int addr = isAbsolute ? addrOrOffset : (ProcessMemory.BA == IntPtr.Zero ? 0 : ProcessMemory.BA.ToInt32() + addrOrOffset);

                Console.WriteLine($"[DBG] DebugReadDoubleAt: isAbsolute={isAbsolute}, param=0x{addrOrOffset:X}, computedAddr=0x{addr:X}");
                if (addr == 0)
                {
                    Console.WriteLine("[DBG] computed address == 0 (BA==0 ?)");
                    return;
                }

                var buf = ProcessMemory.ReadBytes(addr, 8);
                if (buf == null)
                {
                    Console.WriteLine($"[DBG] ReadBytes returned null for 0x{addr:X}");
                    return;
                }
                if (buf.Length < 8)
                {
                    Console.WriteLine($"[DBG] ReadBytes returned {buf.Length} bytes for 0x{addr:X}: {BitConverter.ToString(buf)}");
                    return;
                }

                Console.WriteLine($"[DBG] RAW @0x{addr:X}: {BitConverter.ToString(buf)}");
                double d = BitConverter.ToDouble(buf, 0);
                d = Math.Abs(d);
                UInt16 val = Convert.ToUInt16(d * scale);
                byte[] bval = BitConverter.GetBytes(val);

                Console.WriteLine(
                     $"[DEBUG] ReadScaledDoubleToBuffer: double value={d}, scaled={val}, addrOrOffset=0x{addrOrOffset:X},  addr=0x{addr:X}"
                );

                Array.Copy(bval, 0, out_buffer, outIndex, 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DBG] Исключение ReadScaledDoubleToBuffer: {ex.Message}");
            }
        }
        //=============================================================================
        //Новая функция для напряжения
        public static void ReadScaledDoubleToBuffer1(int absAddr, int outIndex, double scale)
        {
            // автоматически считаем офсет от BaseAddress
            long realAddr = (long)absAddr;
            long offset = absAddr - (long)ProcessMemory.BA;

            byte[] buf = ProcessMemory.ReadBytes((int)realAddr, 8);

            double d = BitConverter.ToDouble(buf, 0);
            d = Math.Abs(d);

            UInt16 val = Convert.ToUInt16(d * scale);
            byte[] bval = BitConverter.GetBytes(val);

            #if DEBUG
            Console.WriteLine(
                $"[DEBUG] ReadScaledDoubleToBuffer: absAddr=0x{absAddr:X}, " +
                $"BA=0x{ProcessMemory.BA.ToInt64():X}, offset=0x{offset:X}, " +
                $"realAddr=0x{realAddr:X}, value={d}, scaled={val}"
            );
            #endif

            Array.Copy(bval, 0, out_buffer, outIndex, 2);
        }
        //=============================================================================




        // ===============================================================
        // Читаем байт из памяти
        // ===============================================================
        public static byte ReadByteValue(int offset)
        {
            byte[] buf = ProcessMemory.ReadBytes((Int32)ProcessMemory.BA + offset, 1);
            return (buf != null && buf.Length > 0) ? buf[0] : (byte)0;
        }

        // ===============================================================
        // Универсальная функция фильтрации ламп (address — абсолютный адрес)
        // ===============================================================
        public static byte ReadLampStableAbsolute(int address, int retries = 3, int delayMs = 5)
        {
            int ones = 0;
            for (int i = 0; i < retries; i++)
            {
                var buffer = ProcessMemory.ReadBytes(address, 1);
                if (buffer != null && buffer.Length > 0 && buffer[0] == 1) ones++;
                if (delayMs > 0) Thread.Sleep(delayMs);
            }
            return (byte)(ones > 0 ? 1 : 0);
        }

        // ---------------------------------------------------------------
        // Чтение лампы с фильтрацией → в out_buffer (offset относит. к BA)
        // ---------------------------------------------------------------
        public static void ReadLampStableToBuffer(int offset, int outIndex, int retries = 3, int delayMs = 5)
        {
            byte val = ReadLampStableAbsolute((int)ProcessMemory.BA + offset, retries, delayMs);
            out_buffer[outIndex] = val;
        }

        // ===============================================================
        // Хелпер для установки бита
        // Устанавливает значение лампы (0/1) в битовую маску
        // ===============================================================
        public static void SetLampBit(ref uint mask, int lampIndex, byte value)
        {
            if (value == 1)
                mask |= (uint)(1 << lampIndex);
            else
                mask &= ~(uint)(1 << lampIndex);
        }

        // ===============================================================
        // Хелпер для чтения лампы в битовую маску
        // ===============================================================
        public static void ReadLampStableToMask(ref uint mask, int lampIndex, int offset, int retries = 3, int delayMs = 5)
        {
            byte val = ReadLampStableAbsolute((int)ProcessMemory.BA + offset, retries, delayMs);
            SetLampBit(ref mask, lampIndex, val);
        }

        // ===============================================================
        // Универсальная функция для чтения ламп с фильтрацией.
        // Она внутри прочитает N раз, подождёт при необходимости,
        // и вернёт «устойчивое» значение.
        // ===============================================================
        private static byte ReadLampStable(int address, int retries = 3, int delayMs = 5)
        {
            int ones = 0;

            for (int i = 0; i < retries; i++)
            {
                var buffer = ProcessMemory.ReadBytes(address, 1);
                if (buffer[0] == 1) ones++;
                if (delayMs > 0) Thread.Sleep(delayMs);
            }

            // если хотя бы один раз получили "1" — считаем что лампа горит
            return (byte)(ones > 0 ? 1 : 0);
        }
    }
}

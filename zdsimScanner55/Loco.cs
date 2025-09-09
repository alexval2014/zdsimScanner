using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace zdsimScanner
{
    class Loco
    {
        // REQUIRED CONSTS
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int MEM_COMMIT = 0x00001000;
        const int PAGE_READWRITE = 0x04;
        const int PROCESS_VM_READ = 0x0010;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;


        // REQUIRED METHODS
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(
            int hProcess,
            int lpBaseAddress,
            byte[] lpBuffer,
            int nSize,
            ref int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        //Ищем окно для отправки клавиши
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        //Фокус на окно
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        // REQUIRED STRUCTS
        public struct MEMORY_BASIC_INFORMATION
        {
            public int BaseAddress;
            //public int AllocationBase;
            //public int AllocationProtect;
            //public int RegionSize;
            //public int State;
            //public int Protect;
            //public int lType;
        }

        public struct SYSTEM_INFO
        {
            public ushort processorArchitecture;
            ushort reserved;
            public uint pageSize;
            public IntPtr minimumApplicationAddress;
            public IntPtr maximumApplicationAddress;
            public IntPtr activeProcessorMask;
            public uint numberOfProcessors;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;
        }
        
        public static int P_ID;
        public static int P_HANDLE;
        public static Process current_process;

        public static IntPtr BA;
        public static IntPtr BA2;
        public static IntPtr BA3;
        public static IntPtr BA_openal32;

        public static int i_process_name = 0;//процесс  6-zdsim54.006
        public static int i_baze_address_flag_fail = 0;//процесс 1-zlauncher
        public static int i_module_select = 0;         //модуль процесса exe,dll,
        public static int sig_pos_pnevm = 0;
        public static int sig_pos_elektro = 0;

        public static int sig_loco = 0;            //тип локомотива
        public static int locoCode = 0;            //код локомотива

        public static float i_skor_tek_convert;
        public static float i_tok_ept_convert;
        public static float i_napruga_ks_convert;
        public static float i_napruga_td_convert;
        public static float i_tok_convert;
        public static float i_pnevmo_convert;
        public static UInt16 i_bdit_current;        //бдительность
        public static int i_dvery_current = 0;      //двери 0-откр,1-закр.
        public static int i_lampa_LK = 0;           //для секций
        public static int i_lampa_LK_flag = 0;      //для секций
        public static string s_path_zdsim_sound;    //путь звуков zdsim

        //пишем в локомотив
        public static int i_temp_buffer_count1 = 0;//переменная счетчика массивов

        //буфера флагов звуков кнопок, если 1, то флаг взведен и звук запрещен
        public static int[] Controls_flag_key_buffer = new int[34];
        public static int[] Neshtatki_flag_key_buffer = new int[100];
        public static int[] ES5K_flag_key_buffer = new int[109];
        public static int[] EP1M_flag_key_buffer = new int[112];
        public static int[] CHS2K_flag_key_buffer = new int[32];
        public static int[] CHS4_flag_key_buffer = new int[55];
        public static int[] CHS4KVR_flag_key_buffer = new int[55];
        public static int[] CHS4T_flag_key_buffer = new int[54];
        public static int[] CHS7_flag_key_buffer = new int[46];
        public static int[] CHS8_flag_key_buffer = new int[63];
        public static int[] VL11M_flag_key_buffer = new int[84];
        public static int[] VL82M_flag_key_buffer = new int[85];
        public static int[] VL80T_flag_key_buffer = new int[49];
        public static int[] VL85_flag_key_buffer = new int[82];
        public static int[] TEP70_flag_key_buffer = new int[36];
        public static int[] TE10U_flag_key_buffer = new int[47];
        public static int[] M62_flag_key_buffer = new int[36];
        public static int[] ED4M_flag_key_buffer = new int[33];
        public static int[] ED9M_flag_key_buffer = new int[30];
        public static int[] tem18_flag_key_buffer = new int[32];
        //------------------------------------------------------------------------------------
        public static bool open_process(string p_name)
        {
            Process[] p = Process.GetProcessesByName(p_name);

            if (p.Length == 0) return false;

            current_process = p[0];
            var h = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, current_process.Id);

            if ((int)h != 0)
            {
                P_ID = current_process.Id;
                P_HANDLE = (int)h;
                return true;
            }
            return false;
        }

        //------------------------------------------------------------------------------------
        public static byte[] read_bytes(Int32 addr, int size)
        {
            int bytesRead = 0;  // количество байтов, считанных с помощью ReadProcessMemory
            byte[] buffer = new byte[size];
            ReadProcessMemory((int)P_HANDLE, addr, buffer, size, ref bytesRead);
            return buffer;
        }

        //------------------------------------------------------------------------------------
        public static void write_bytes(Int32 addr, byte[] buffer)
        {
            int bytesRead = 0;
            WriteProcessMemory((int)P_HANDLE, addr, buffer, buffer.Length, ref bytesRead);
        }

        //------------------------------------------------------------------------------------
        //Читаем указатель
        public static int read_pointer(Int32 baze_address, int[] offsets)
        {
            baze_address = BitConverter.ToInt32(Loco.read_bytes(baze_address, 4), 0);

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
                    i_baze_address_flag_fail = 1;
                    return 0;
                }
                else
                {
                    baze_address = BitConverter.ToInt32(read_bytes(baze_address, 4), 0);
                }
            }
            return baze_address;
        }

        // ===============================================================
        // Чтение 1 байта
        // ===============================================================
        public static void ReadByteToBuffer(int offset, int outIndex, byte[] cache = null)
        {
            byte[] buf = read_bytes((Int32)BA + offset, 1);
            byte val = (buf != null && buf.Length > 0) ? buf[0] : (byte)0;

            // копируем в выходной буфер
            out_buffer[outIndex] = val;

            // при необходимости сохраняем в кэш (например, b_temp_byte1)
            if (cache != null) cache[0] = val;
        }

        // ===============================================================
        // Чтение 2 байт (Int16/UInt16)
        // ===============================================================
        public static void ReadUInt16ToBuffer(int offset, int outIndex)
        {
            byte[] buf = read_bytes((Int32)BA + offset, 2);
            Array.Copy(buf, 0, out_buffer, outIndex, 2);
        }

        // ===============================================================
        // Чтение 4 байт (float → UInt16 → 2 байта)
        // ===============================================================
        public static void ReadScaledFloatToBuffer(int offset, int outIndex, float scale)
        {
            byte[] buf = read_bytes((Int32)BA + offset, 4);
            float f = BitConverter.ToSingle(buf, 0);
            f = Math.Abs(f);
            UInt16 val = Convert.ToUInt16(f * scale);

            byte[] bval = BitConverter.GetBytes(val);
            Array.Copy(bval, 0, out_buffer, outIndex, 2);
        }

        // ===============================================================
        // Чтение double → UInt16 → 2 байта
        // ===============================================================
        public static void ReadScaledDoubleToBuffer(int offset, int outIndex, double scale)
        {
            byte[] buf = read_bytes((Int32)BA + offset, 8);
            double d = BitConverter.ToDouble(buf, 0);
            d = Math.Abs(d);
            UInt16 val = Convert.ToUInt16(d * scale);

            byte[] bval = BitConverter.GetBytes(val);
            Array.Copy(bval, 0, out_buffer, outIndex, 2);
        }

        public static byte ReadByteValue(int offset)
        {
            byte[] buf = read_bytes((Int32)BA + offset, 1);
            return (buf != null && buf.Length > 0) ? buf[0] : (byte)0;
        }











        //------------------------------------------------------------------------------------
        public static void InitBuffers()
        {
            out_buffer = new byte[64];
            in_buffer = new byte[64];
            temp_buffer = new byte[128];
            temp_buffer2 = new byte[128];
            temp_buffer3 = new byte[128];
        }

        //коллекция массивов звуков, для zdsim
        public static List<byte[]> list_collect_sounds_zdsim_controls = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_neshtatki = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_es5k = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_ep1m = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_chs2k = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_chs4 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_chs4t = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_chs4kvr = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_chs7 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_chs8 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_vl11 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_vl82 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_vl80t = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_vl85 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_tep70 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_te10u = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_m62 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_ed4m = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_ed9m = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_tem18 = new List<byte[]>();
        
        public static byte[] out_buffer = new byte[64];
        public static byte[] out_buffer1 = new byte[128];
        public static byte[] in_buffer = new byte[64];
        public static byte[] temp_buffer = new byte[128];
        public static byte[] temp_buffer2 = new byte[128];
        public static byte[] temp_buffer3 = new byte[128];
        public static byte[] temp_buffer4 = new byte[128];
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
            SYSTEM_INFO sys_info = new SYSTEM_INFO();
            GetSystemInfo(out sys_info);

            /******* Список модулей процесса *******/
            List<ProcessModule> m_module = new List<ProcessModule>();

            int i_name = 0;
            for (int i = 0; i < current_process.Modules.Count; i++)
            {
                m_module.Add(current_process.Modules[i]);

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

            /******* Получаем базовый адрес модуля *******/
            IntPtr proc_min_address = sys_info.minimumApplicationAddress;
            IntPtr proc_max_address = sys_info.maximumApplicationAddress;

            //сохранение значений в виде длинных целых чисел, так что мне не придется делать много приведений позже
            long proc_min_address_l = (long)proc_min_address;
            long proc_max_address_l = (long)proc_max_address;

            if (i_name == 2)
            {
                Process process = Process.GetProcessesByName("launcher")[0];
                //открытие процесса с желаемым уровнем доступа
                IntPtr processHandle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, process.Id);
            }

            sig_loco = 0;

            //Читаем из памяти процеса игры версию ZD simulator
            temp_buffer = read_bytes((int)BA + 0x0000011d, 1);

            // ---------------------- V54.006 --------------------------
            if (temp_buffer[0] == 0x9c)
            {
                i_process_name = 6; // версия 54.006

                //читаем путь zdsim
                int i_flag_string = 0;
                for (int i = 0; i_flag_string < 2; i++)
                {
                    temp_buffer = read_bytes((Int32)BA_openal32 + 0x00dacfc + i, 1);
                    if (temp_buffer[0] != 0)
                    {
                        i_flag_string = 0;
                        temp_buffer2[i] = temp_buffer[0];
                    }
                    else
                    {
                        i_flag_string++;
                    }

                    temp_buffer3 = new byte[i];
                    Array.Copy(temp_buffer2, 0, temp_buffer3, 0, i);
                }

                s_path_zdsim_sound = Encoding.ASCII.GetString(temp_buffer3);
                s_path_zdsim_sound = s_path_zdsim_sound.Remove(s_path_zdsim_sound.LastIndexOf(@"\Lau") + 1);
                s_path_zdsim_sound = s_path_zdsim_sound + "sound";

                locoCode = BitConverter.ToInt32(read_bytes((int)BA + 0x4F711B4, 4), 0);

                Console.WriteLine($"[DEBUG] V54.006 locoCode = {locoCode} (0x{locoCode:X})");
               

                if (locoSignatures.TryGetValue(locoCode, out int locoId))
                {
                    sig_loco = locoId;
                    Console.WriteLine($"[DEBUG] Локомотив найден: sig_loco = {sig_loco}");
                }
                else
                {
                    Console.WriteLine("[DEBUG] Локомотив в словаре не найден!");
                }
            }

            // ---------------------- V55.008 --------------------------
            if (temp_buffer[0] == 0x68)
            {
                i_process_name = 7; // версия 55.008
                locoCode = BitConverter.ToInt32(read_bytes((int)BA + 0x4F8D93C, 4), 0);

                Console.WriteLine($"[DEBUG] V55.008 locoCode = {locoCode} (0x{locoCode:X})");

                if (locoSignatures.TryGetValue(locoCode, out int locoId))
                {
                    sig_loco = locoId;
                    Console.WriteLine($"[DEBUG] Локомотив найден: sig_loco = {sig_loco}");
                }
                else
                {
                    Console.WriteLine("[DEBUG] Локомотив в словаре не найден!");
                }
            }

            InitBuffers();
            return sig_loco;
        }
        //===================================================================================
        //-----------------------------------------------------------------------------------
        // Ищем указатель пневматики
        //-----------------------------------------------------------------------------------
        public static int find_pnevm()
        {
            Console.WriteLine("Поиск пневматики...");

            //статические ZD simulator v54.006
            if (i_process_name == 6)
            {
                temp_buffer = read_bytes((Int32)BA + 0x04F7114C, 4);
            }

            //статические ZD simulator v55.008
            if (i_process_name == 7)
            {
                temp_buffer = read_bytes((Int32)BA + 0x4F8D8D4, 4);
            }

            sig_pos_pnevm = BitConverter.ToInt32(temp_buffer, 0);
            Console.WriteLine(sig_pos_pnevm);
            return sig_pos_pnevm;
        }

        //-----------------------------------------------------------------------------------
        // Ищем указатель электрики
        //-----------------------------------------------------------------------------------
        public static int find_elektro()
        {

            //Console.WriteLine("Поиск электрики...");

            //статические ZD simulator v54.006
            if (i_process_name == 6)
            {
                temp_buffer = read_bytes((Int32)BA + 0x062531B4, 4);
            }

            sig_pos_elektro = BitConverter.ToInt32(temp_buffer, 0);
            return sig_pos_elektro;
        }


    }
}
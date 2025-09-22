using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using zdsimScanner.Models;
using zdsimScanner.NativeApi;

namespace zdsimScanner
{
    class Loco
    {
        public static IntPtr P_HANDLE;   // handle
        public static int P_ID;          // id процесса
        public static Process current_process;

        public static IntPtr BA;
        public static IntPtr BA2;
        public static IntPtr BA3;
        public static IntPtr BA_openal32;

        public static int i_process_name = 0;             //процесс  6-zdsim54.006
        public static int i_baze_address_flag_fail = 0;   //процесс 1-zlauncher
        public static int i_module_select = 0;            //модуль процесса exe,dll,
        public static int sig_pos_pnevm = 0;
        public static int sig_pos_elektro = 0;

        public static int locoCode = 0;                      // код локомотива (сырые числа из памяти)
        public static LocoType sig_loco2 = LocoType.Unknown; // тип локомотива (enum)
        public static int sig_loco = 0;                      // тип локомотива старый формат

        //Множетили приборов
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

        public static bool AttachToProcess(int processId)
        {
            P_ID = processId;

            IntPtr hProcess = NativeMethods.OpenProcess(
            NativeMethods.PROCESS_QUERY_INFORMATION |
            NativeMethods.PROCESS_VM_READ |
            NativeMethods.PROCESS_VM_WRITE |
            NativeMethods.PROCESS_VM_OPERATION,
            false,
            current_process.Id);

            if (hProcess == IntPtr.Zero)
            {
                int err = Marshal.GetLastWin32Error();
                Console.WriteLine($"OpenProcess failed, error {err}");
                return false;
            }

            P_HANDLE = hProcess;
            P_ID = current_process.Id;
            return true;
        }

        // При завершении работы с процессом вызываешь Loco.Detach() — чтобы закрыть handle.
        public static void Detach()
        {
            if (P_HANDLE != IntPtr.Zero)
            {
                NativeMethods.CloseHandle(P_HANDLE);
                P_HANDLE = IntPtr.Zero;
            }
        }

        //------------------------------------------------------------------------------------
        // Открываем процес игрового симулятора 
        //------------------------------------------------------------------------------------
        public static bool open_process(string p_name)
        {
            Process[] p = Process.GetProcessesByName(p_name);
            if (p.Length == 0) return false;

            current_process = p[0];

            IntPtr hProcess = NativeMethods.OpenProcess(
                NativeMethods.PROCESS_QUERY_INFORMATION |
                NativeMethods.PROCESS_VM_READ |
                NativeMethods.PROCESS_VM_WRITE |
                NativeMethods.PROCESS_VM_OPERATION,
                false,
                current_process.Id);

            if (hProcess == IntPtr.Zero)
            {
                int err = Marshal.GetLastWin32Error();
                Console.WriteLine($"OpenProcess failed, error {err}");
                return false;
            }

            P_ID = current_process.Id;

            P_HANDLE = hProcess;   // ✅ теперь IntPtr

            return true;
        }
    }
}
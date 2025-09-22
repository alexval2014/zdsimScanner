using System;
using System.Runtime.InteropServices;

namespace zdsimScanner.NativeApi
{
    /// <summary>
    /// Нативные вызовы WinAPI (ReadProcessMemory, WriteProcessMemory, работа с процессами,
    /// памятью и окнами). Расширена версия: добавлены CloseHandle и константы прав доступа.
    /// Подписи оставлены совместимыми с существующим кодом.
    /// </summary>
    internal static class NativeMethods
    {
        // -------------------------------
        // Процессы
        // -------------------------------
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        // Константы доступа для OpenProcess (используй вместо магических чисел)
        public const int PROCESS_VM_READ = 0x0010;
        public const int PROCESS_VM_WRITE = 0x0020;
        public const int PROCESS_VM_OPERATION = 0x0008;
        public const int PROCESS_QUERY_INFORMATION = 0x0400;

        const int MEM_COMMIT = 0x00001000;
        const int PAGE_READWRITE = 0x04;

        // Собираем «удобный» вариант полного доступа (по необходимости можно расширить)
        public const int PROCESS_ALL_ACCESS = PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION | PROCESS_QUERY_INFORMATION;

        // -------------------------------
        // Память
        // -------------------------------
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        byte[] lpBuffer,
        int dwSize,
        ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        byte[] lpBuffer,
        int nSize,
        ref int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        // -------------------------------
        // Окна
        // Ищем окно для отправки клавиши
        // -------------------------------
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        //Фокус на окно
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        // -------------------------------
        // Структуры (упрощённые — как в твоём проекте)
        // -------------------------------
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            // у тебя в проекте было int BaseAddress — оставил так для совместимости
            public int BaseAddress;
        }

        [StructLayout(LayoutKind.Sequential)]
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
    }
}

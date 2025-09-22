using System;

namespace zdsimScanner.Utils
{
    public static class Logger
    {
        public static void Info(string s) => Console.WriteLine($"[INFO] {s}");
        public static void Debug(string s) => Console.WriteLine($"[DEBUG] {s}");
        public static void Error(string s) => Console.WriteLine($"[ERROR] {s}");
    }
}

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Production_Calculator
{
    class Memory
    {
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, long lpBaseAddress, byte[] lpBuffer, int dwSize, IntPtr lpNumberOfBytesRead);

        private static byte[] ReadAddress(Process process, long lpBaseAddress, int bytes)
        {
            byte[] buffer = new byte[bytes];
            ReadProcessMemory(process.Handle.ToInt32(), lpBaseAddress, buffer, bytes, IntPtr.Zero);
            return buffer;
        }

        public static int ReadInt32(Process process, long lpBaseAddress)
        {
            return BitConverter.ToInt32(ReadAddress(process, lpBaseAddress, 4), 0);
        }

        public static long ReadInt64(Process process, long lpBaseAddress)
        {
            return BitConverter.ToInt64(ReadAddress(process, lpBaseAddress, 8), 0);
        }
    }
}
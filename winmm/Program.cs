using System;
using System.Runtime.InteropServices;
using System.Text;

namespace winmm
{
    internal class Program
    {
        [DllImport("winmm.dll")]
        private static extern int mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        [DllImport("winmm.dll")]
        private static extern bool mciGetErrorString(int errCode, StringBuilder errMsg, int buflen);
        private StringBuilder sb;
        private Program()
        {
            sb = new StringBuilder(128);
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                string line = Console.ReadLine();
                int error = mciSendString(line, sb, sb.Capacity, default);
                if (error == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("(0) " + sb.ToString());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    mciGetErrorString(error, sb, sb.Capacity);
                    Console.WriteLine("(" + error + ") " + sb.ToString());
                }
            }
        }
        private static void Main(string[] args)
        {
            new Program();
        }
    }
}

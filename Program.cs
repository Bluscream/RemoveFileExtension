using Bluscream;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace RemoveFileExtension
{
    class Program
    {
        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] int nCmdShow);

        static void Main(string[] _args)
        {
            IntPtr handle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(handle, 6);
            // _args = new string[] { "self", @"S:\Steam\steamapps\common\VRChat\Mods\LuaLoader.dll.disabled" };
            var args = _args.ToList();
            args.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location);
            // args.RemoveAt(0);
            var msg = new System.Text.StringBuilder();
            foreach (var arg in args)
            {
                var file = new System.IO.FileInfo(arg);
                var newName = file.FileNameWithoutExtension();
                try {
                    file.Rename(newName);
                } catch (Exception ex)
                {
                    msg.AppendFormat("{0} - > {1}: {2}", file.Name.Quote(), newName.Quote(), ex.Message).AppendLine();
                }
            }
            if (msg.Length > 0)
            {
                ShowWindow(handle, 9);
                Console.WriteLine(msg.ToString());
                Console.ReadLine();
            }
        }
    }
}

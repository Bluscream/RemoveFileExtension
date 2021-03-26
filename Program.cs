using Bluscream;
using System;
// using System.Linq;
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
            // var args = _args.ToList();
            // args.RemoveAt(0);
            var errors = new System.Text.StringBuilder(string.Join(Environment.NewLine, _args));
            foreach (var arg in _args)
            {
                try
                {
                    if (arg == System.Reflection.Assembly.GetExecutingAssembly().Location) continue;
                    var file = new System.IO.FileInfo(arg);
                    var newName = file.FileNameWithoutExtension();
                    errors.AppendFormat("{0} - > {1}: ", file.Name.Quote(), newName.Quote());
                    file.Rename(newName);
                    errors.Append("Success");
                } catch (Exception ex)
                {
                    errors.Append(ex.Message);
                }
                errors.AppendLine();
            }
            if (errors.Length > 0)
            {
                ShowWindow(handle, 9);
                Console.WriteLine(errors.ToString());
                Console.ReadLine();
            }
        }
    }
}

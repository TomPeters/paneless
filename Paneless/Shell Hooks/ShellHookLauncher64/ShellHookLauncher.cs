using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using Paneless.Common;

namespace ShellHookLauncher
{
    public static class ShellHookLauncher
    {
        static void Main(string[] args)
        {
            SetDllDirectory(DirectoryFinder.FindDirectoryInAncestors(ShellHookHelper.DllFileName));
            IntPtr wndProcHook = ShellHookHelper.SetupWndProcHook();
            ShellHookHelper.SetupGetMsgHook();
            using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", ShellHookHelper.PanelessNamedPipe, PipeDirection.Out))
            {
                pipeClient.Connect();
                using (StreamWriter streamWriter = new StreamWriter(pipeClient))
                {
                    // It looks like SetupWndProcHook and SetupGetMsgHook both return the same HHOOK 
                    // (implying it is associated with the dll rather than the callback)
                    // So we only need to return one of these HHOOK values to be unregistered later
                    streamWriter.Write(wndProcHook); 
                }
            }
        }

        //http://msdn.microsoft.com/en-us/library/ms686203%28VS.85%29.aspx
        //http://www.pinvoke.net/default.aspx/kernel32.setdlldirectory
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetDllDirectory(string lpPathName);
    }
}

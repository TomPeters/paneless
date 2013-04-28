using System.Runtime.InteropServices;
using System.Threading;
using Paneless.Common;

namespace ShellHookLauncher
{
    public static class ShellHookLauncher
    {
        private const int Timeout = 1000;
        static void Main(string[] args)
        {
            SetDllDirectory(DirectoryFinder.FindDirectoryInAncestors(ShellHookHelper.DllFileName));
            ShellHookHelper.SetupWndProcHook();
            ShellHookHelper.SetupGetMsgHook();
            while (true)
            {
                Thread.Sleep(Timeout); // Keep this thread alive but don't want it to consume much CPU time
            }
        }

        //http://msdn.microsoft.com/en-us/library/ms686203%28VS.85%29.aspx
        //http://www.pinvoke.net/default.aspx/kernel32.setdlldirectory
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetDllDirectory(string lpPathName);
    }
}

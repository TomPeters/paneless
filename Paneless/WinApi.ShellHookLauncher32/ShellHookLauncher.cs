using System.Runtime.InteropServices;
using System.Threading;

namespace WinApi.ShellHookLauncher32
{
    public static class ShellHookLauncher
    {
        static void Main(string[] args)
        {
            SetDllDirectory(@"C:/Programming/paneless/lib"); // TODO: Add more flexible directory searching to find lib directory with appropriate dlls
            SetupWndProcHook();
            SetupGetMsgHook();
            while (true)
            {
                Thread.Sleep(100000); // Keep this thread alive but don't want it to consume much CPU time
            }
        }

        #region HookSetupMethods

        [DllImport("WinApi.ShellHook32.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool SetupWndProcHook();

        [DllImport("WinApi.ShellHook32.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool SetupGetMsgHook();

        //http://msdn.microsoft.com/en-us/library/ms686203%28VS.85%29.aspx
        //http://www.pinvoke.net/default.aspx/kernel32.setdlldirectory
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetDllDirectory(string lpPathName);

        #endregion
    }
}

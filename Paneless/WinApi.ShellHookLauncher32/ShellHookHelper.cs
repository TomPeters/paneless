using System.Runtime.InteropServices;

namespace WinApi.ShellHookLauncher64
{
    internal static class ShellHookHelper
    {
        public const string DllFileName = "WinApi.ShellHook32.dll";

        #region HookSetupMethods

        [DllImport(DllFileName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetupWndProcHook();

        [DllImport(DllFileName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetupGetMsgHook();

        #endregion
    }
}

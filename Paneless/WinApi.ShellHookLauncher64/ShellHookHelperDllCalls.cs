using System.Runtime.InteropServices;

namespace WinApi.ShellHookLauncher64
{
    internal static partial class ShellHookHelper
    {
        [DllImport(DllFileName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetupWndProcHook();

        [DllImport(DllFileName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetupGetMsgHook();
    }
}
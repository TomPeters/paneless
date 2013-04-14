using System.Runtime.InteropServices;

namespace ShellHookLauncher
{
    internal static partial class ShellHookHelper
    {
        [DllImport(DllFileName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetupWndProcHook();

        [DllImport(DllFileName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetupGetMsgHook();
    }
}
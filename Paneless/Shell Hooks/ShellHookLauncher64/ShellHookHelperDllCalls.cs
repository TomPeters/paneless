using System;
using System.Runtime.InteropServices;

namespace ShellHookLauncher
{
    internal static partial class ShellHookHelper
    {
        [DllImport(DllFileName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SetupWndProcHook();

        [DllImport(DllFileName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SetupGetMsgHook();
    }
}
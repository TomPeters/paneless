using System;
using System.Runtime.InteropServices;

namespace Paneless.WinApi.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CWPSTRUCT
    {
        public IntPtr lparam;
        public IntPtr wparam;
        public int message;
        public IntPtr hwnd;
    }
}

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Paneless.WinApi
{
    internal static class WinApi
    {
        //Links
        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms632586(v=vs.85).aspx
        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms632595(v=vs.85).aspx
        //http://msdn.microsoft.com/en-us/library/windows/desktop/ff468925(v=vs.85).aspx

        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms633499(v=vs.85).aspx
        //http://www.pinvoke.net/default.aspx/user32/FindWindow.html
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms633520(v=vs.85).aspx
        //http://www.pinvoke.net/default.aspx/user32/GetWindowText.html
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms633521(v=vs.85).aspx
        //http://www.pinvoke.net/default.aspx/user32/GetWindowTextLength.html
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms633582(v=vs.85).aspx
        //http://www.pinvoke.net/default.aspx/user32.getclassname
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr windowPtr, StringBuilder titleStringBuilder,
                                              int classNameLength);

        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms633519(v=vs.85).aspx
        //http://www.pinvoke.net/default.aspx/user32.getwindowrect
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr windowPtr, RECT rect);

        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms633545(v=vs.85).aspx
        //http://www.pinvoke.net/default.aspx/user32.setwindowpos
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        //http://msdn.microsoft.com/en-us/library/ms633497(VS.85).aspx
        //http://www.pinvoke.net/default.aspx/user32.enumwindows
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(WindowsEnumProcess lpEnumFunc, IntPtr lParam);

        //http://msdn.microsoft.com/en-us/library/ms633518%28VS.85%29.aspx
        //http://www.pinvoke.net/default.aspx/user32.getwindowplacement
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl);

        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms633530(v=vs.85).aspx
        //http://www.pinvoke.net/default.aspx/user32.iswindowvisible
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms633584(v=vs.85).aspx
        //http://www.pinvoke.net/default.aspx/user32.getwindowlong
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    }
}

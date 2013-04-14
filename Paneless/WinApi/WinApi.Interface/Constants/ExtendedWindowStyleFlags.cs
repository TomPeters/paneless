using System;

//http://msdn.microsoft.com/en-us/library/windows/desktop/ff700543(v=vs.85).aspx
//http://www.pinvoke.net/default.aspx/user32.getwindowlong
namespace WinApi.Interface.Constants
{
    [Flags]
    public enum ExtendedWindowStyleFlags
    {
        WS_EX_DLGMODALFRAME = 0x0001,
        WS_EX_NOPARENTNOTIFY = 0x0004,
        WS_EX_TOPMOST = 0x0008,
        WS_EX_ACCEPTFILES = 0x0010,
        WS_EX_TRANSPARENT = 0x0020,
        WS_EX_MDICHILD = 0x0040,
        WS_EX_TOOLWINDOW = 0x0080,
        WS_EX_WINDOWEDGE = 0x0100,
        WS_EX_CLIENTEDGE = 0x0200,
        WS_EX_CONTEXTHELP = 0x0400,
        WS_EX_RIGHT = 0x1000,
        WS_EX_LEFT = 0x0000,
        WS_EX_RTLREADING = 0x2000,
        WS_EX_LTRREADING = 0x0000,
        WS_EX_LEFTSCROLLBAR = 0x4000,
        WS_EX_RIGHTSCROLLBAR = 0x0000,
        WS_EX_CONTROLPARENT = 0x10000,
        WS_EX_STATICEDGE = 0x20000,
        WS_EX_APPWINDOW = 0x40000,
        WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE),
        WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST),
        WS_EX_LAYERED = 0x00080000,
        WS_EX_NOINHERITLAYOUT = 0x00100000,
        WS_EX_LAYOUTRTL = 0x00400000,
        WS_EX_COMPOSITED = 0x02000000,
        WS_EX_NOACTIVATE = 0x08000000,
    }
}

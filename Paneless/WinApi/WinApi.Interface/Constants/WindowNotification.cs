namespace WinApi.Interface.Constants
{
    // http://msdn.microsoft.com/en-us/library/windows/desktop/ff468922(v=vs.85).aspx
    public enum WindowNotification : uint
    {
        WM_CREATE = 0x0001,
        WM_DESTROY = 0x0002,
        WM_MOVING = 0x0216,
        WM_MOVE = 0x0003,
        WM_SHOWWINDOW = 0x0018,
        WM_SIZING = 0x0214,
        WM_SIZE = 0x0005
    }
}

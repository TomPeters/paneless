using System;

namespace Paneless.WinApi
{
    public delegate bool WindowsEnumProcess(int hWnd, int lParam);

    public class DesktopManager : IDesktopManager
    {
        public void EnumWindows(WindowsEnumProcess windowsEnumCallBack)
        {
            WinApi.EnumWindows(windowsEnumCallBack, IntPtr.Zero);
        }
    }

    public interface IDesktopManager
    {
        void EnumWindows(WindowsEnumProcess windowsEnumCallBack);
    }
}

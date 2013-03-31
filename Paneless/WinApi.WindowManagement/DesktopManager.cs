using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Paneless.WinApi
{
    public delegate bool WindowsEnumProcess(int hWnd, int lParam);

    public class DesktopManager : IDesktopManager
    {
        private Process _hookThread32;
        private Process _hookThread64;
        private const string PanelessWindowPropertyId = "PANELESS_WND_40FB6774-53A9-4341-9FF7-BAD24A8205C6";

        public void EnumWindows(WindowsEnumProcess windowsEnumCallBack)
        {
            WinApi.EnumWindows(windowsEnumCallBack, IntPtr.Zero);
        }

        public bool SetupWindowsHook(IntPtr shellWindowPtr)
        {
            bool result = StoreWindowPtr(shellWindowPtr);
            _hookThread32 = Process.Start(@"C:\Programming\paneless\lib\WinApi.ShellHookLauncher32.exe"); // TODO: Add more flexible directory searching
            _hookThread64 = Process.Start(@"C:\Programming\paneless\lib\WinApi.ShellHookLauncher64.exe");
            return result;
        }

        public void TerminateHookThreads()
        {
            _hookThread32.Kill();
            _hookThread64.Kill();
        }

        public int RegisterWindowMessage(string windowMessage)
        {
            return (int)WinApi.RegisterWindowMessage(windowMessage);
        }

        private bool StoreWindowPtr(IntPtr windowPtr)
        {
            return WinApi.SetProp(WinApi.GetDesktopWindow(), PanelessWindowPropertyId, windowPtr);
        }
    }

    public interface IDesktopManager
    {
        void EnumWindows(WindowsEnumProcess windowsEnumCallBack);
        bool SetupWindowsHook(IntPtr shellWindowPtr);
        int RegisterWindowMessage(string windowMessage);
        void TerminateHookThreads();
    }
}

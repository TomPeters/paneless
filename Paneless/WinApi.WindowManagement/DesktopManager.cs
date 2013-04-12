using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Paneless.WinApi
{
    public delegate bool WindowsEnumProcess(int hWnd, int lParam);

    public class DesktopManager : IDesktopManager
    {
        private readonly IDirectoryFinder _directoryFinder;
        private Process _hookThread32;
        private Process _hookThread64;
        private const string HookLauncher32 = "WinApi.ShellHookLauncher32.exe";
        private const string HookLauncher64 = "WinApi.ShellHookLauncher64.exe";
        private const string PanelessWindowPropertyId = "PANELESS_WND_40FB6774-53A9-4341-9FF7-BAD24A8205C6";

        public DesktopManager()
            : this(new DirectoryFinder())
        {
        }

        private DesktopManager(IDirectoryFinder directoryFinder)
        {
            _directoryFinder = directoryFinder;
        }

        public void EnumWindows(WindowsEnumProcess windowsEnumCallBack)
        {
            WinApi.EnumWindows(windowsEnumCallBack, IntPtr.Zero);
        }

        // TODO: Make this method testable
        // TODO: Add more flexible directory searching
        // TODO: Add architecture detection to only launcher require shell hooks
        public bool SetupWindowsHook(IntPtr shellWindowPtr)
        {
            bool result = StoreWindowPtr(shellWindowPtr);
            _hookThread32 = Process.Start(_directoryFinder.FindDirectoryInAncestors(HookLauncher32));
            _hookThread64 = Process.Start(_directoryFinder.FindDirectoryInAncestors(HookLauncher64));
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

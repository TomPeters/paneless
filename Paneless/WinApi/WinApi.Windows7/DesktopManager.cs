using System;
using System.Diagnostics;
using Common;
using WinApi.Interface;

namespace WinApi.Windows7
{
    public class DesktopManager : IDesktopManager
    {
        private Process _hookThread32;
        private Process _hookThread64;
        private readonly bool _arch64;

        public DesktopManager()
        {
            _arch64 = Is64Bit();
        }

        public void EnumWindows(WindowsEnumProcess windowsEnumCallBack)
        {
            WinApi.EnumWindows(windowsEnumCallBack, IntPtr.Zero);
        }

        // TODO: Make this method testable
        public bool SetupWindowsHook(IntPtr shellWindowPtr)
        {
            bool result = StoreWindowPtr(shellWindowPtr);
            _hookThread32 = Process.Start(DirectoryFinder.FindDirectoryInAncestors(Identifiers.HookLauncher32));
            if(_arch64) _hookThread64 = Process.Start(DirectoryFinder.FindDirectoryInAncestors(Identifiers.HookLauncher64));
            return result;
        }

        public void TerminateHookThreads()
        {
            _hookThread32.Kill();
            if(_arch64) _hookThread64.Kill();
        }

        public bool RegisterHotKeys(IntPtr windowPtr, int keyId, uint modKeys, uint keys)
        {
            return WinApi.RegisterHotKey(windowPtr, keyId, modKeys, keys);
        }

        public bool UnregisterHotKeys(IntPtr windowPtr, int keyId)
        {
            return WinApi.UnregisterHotKey(windowPtr, keyId);
        }

        public int RegisterWindowMessage(string windowMessage)
        {
            return (int)WinApi.RegisterWindowMessage(windowMessage);
        }

        private bool StoreWindowPtr(IntPtr windowPtr)
        {
            return WinApi.SetProp(WinApi.GetDesktopWindow(), Identifiers.PanelessWindowPropertyId, windowPtr);
        }

        private static bool Is64Bit()
        {
            if (IntPtr.Size == 8)
            {
                return true;
            }
            using (Process proc = Process.GetCurrentProcess())
            {
                bool returnValue;
                if (!WinApi.IsWow64Process(proc.Handle, out returnValue))
                {
                    return false;
                }
                return returnValue;
            }
        }
    }
}

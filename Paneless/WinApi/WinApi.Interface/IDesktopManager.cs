using System;

namespace WinApi.Interface
{
    /// <summary>
    /// Responsible for interaction with all other windows api tasks that aren't tileable window specific
    /// This includes things like setup for the hotkey window as well as enumeration of active windows
    /// </summary>
    public interface IDesktopManager
    {
        void EnumWindows(WindowsEnumProcess windowsEnumCallBack);
        bool SetupWindowsHook(IntPtr shellWindowPtr);
        int RegisterWindowMessage(string windowMessage);
        void UnregisterHooks();
        bool RegisterHotKeys(IntPtr windowPtr, int keyId, uint modKeys, uint keys);
        bool UnregisterHotKeys(IntPtr windowPtr, int keyId);
    }

    public delegate bool WindowsEnumProcess(int hWnd, int lParam);
}

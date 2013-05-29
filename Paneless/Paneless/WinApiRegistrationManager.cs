using System;
using Paneless.Core.Events;
using WinApi.Interface;

namespace Paneless
{
    public class WinApiRegistrationManager : IWinApiRegistrationManager
    {
        private const int StartupThreadTerminationTimeout = 50;
        private const int ShutdownThreadTerminationTimeout = 250;

        public WinApiRegistrationManager(IDesktopManager desktopManager)
        {
            DesktopManager = desktopManager;
        }

        private IDesktopManager DesktopManager { get; set; }

        public int RegisterWindowMessage(string windowMessage)
        {
            return DesktopManager.RegisterWindowMessage(windowMessage);
        }

        public void SetupHooks(IntPtr windowPtr)
        {
            DesktopManager.UnregisterHooks(StartupThreadTerminationTimeout); // Shutdown any existing hook threads in case paneless crashed
            DesktopManager.SetupWindowsHook(windowPtr);
        }

        public void UnregisterHooks()
        {
            DesktopManager.UnregisterHooks(ShutdownThreadTerminationTimeout);
        }

        public void SetupHotKeys(IntPtr windowPtr)
        {
            const int MOD_WIN = 0x8;
            const int MOD_CONTROL = 0x2;
            DesktopManager.RegisterHotKeys(windowPtr, (int)HotkeyEvents.Tile, (uint)(MOD_WIN | MOD_CONTROL), 0x54); //T for Tile
            DesktopManager.RegisterHotKeys(windowPtr, (int)HotkeyEvents.Untile, (uint)(MOD_WIN | MOD_CONTROL), 0x55); //U for Untile
        }

        public void UnregisterHotKeys(IntPtr windowPtr)
        {
            DesktopManager.UnregisterHotKeys(windowPtr, (int) HotkeyEvents.Tile);
            DesktopManager.UnregisterHotKeys(windowPtr, (int) HotkeyEvents.Untile);
        }
    }

    public interface IWinApiRegistrationManager
    {
        int RegisterWindowMessage(string windowMessage);
        void SetupHooks(IntPtr windowPtr);
        void UnregisterHooks();
        void SetupHotKeys(IntPtr windowPtr);
        void UnregisterHotKeys(IntPtr windowPtr);
    }
}

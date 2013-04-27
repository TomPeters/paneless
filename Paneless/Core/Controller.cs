using System;
using System.Collections.Generic;
using Paneless.Core;
using Paneless.Core.Events;
using WinApi.Interface;

namespace Paneless
{
    // Orchestrates Core
    public class Controller : IController
    {
        private IDesktop Desktop { get; set; }

        private IWindowManager WindowManager { get; set; }

        private IDesktopManager DesktopManager { get; set; }

        private ILayoutFactory LayoutFactory { get; set; }

        public Controller(ILayoutFactory layoutFactory, IDesktop desktop, IDesktopManager desktopManager, IWindowManager windowManager)
        {
            Desktop = desktop;
            WindowManager = windowManager;
            DesktopManager = desktopManager;
            LayoutFactory = layoutFactory;

            Initialize();
        }

        private void Initialize() //TODO this is temporary and should be put into some kind of settings/config, eg LoadSettingsAndInitialize()
        {
            foreach (IMonitor monitor in Desktop.Monitors)
            {
                ITag newTag = new Tag();
                Desktop.AddTag(newTag);
                monitor.Tag = newTag;
            }

            AssignWindows();
        }

        public void SetLayouts(string layout)
        {
            foreach (IMonitor monitor in Desktop.Monitors)
            {
                monitor.Tag.SetLayout(LayoutFactory.CreateLayout(layout));
            }
            RefreshAllTags();
        }

        public void RefreshAllTags()
        {
            foreach (IMonitor monitor in Desktop.Monitors)
            {
                monitor.Tag.Tile();
            }
        }

        private void AssignWindows() // TODO: This should always be done at start up (is this the same as controller construction?)
        {
            List<IWindow> windows = Desktop.DetectWindows();

            foreach (IWindow window in windows)
            {
                foreach (IMonitor monitor in Desktop.Monitors)
                {
                    if (monitor.IsInSameScreen(window))
                    {
                        monitor.AddWindow(window);
                    }
                }
            }
        }

        #region WinApiSetup
        // TODO Think about moving this to a seperate class - WinApiController?

        public int RegisterWindowMessage(string windowMessage)
        {
            return DesktopManager.RegisterWindowMessage(windowMessage);
        }

        public void SetupHook(IntPtr windowPtr)
        {
            DesktopManager.SetupWindowsHook(windowPtr); // TODO Use return type to check if this succeeded
        }

        public void TerminateHook()
        {
            DesktopManager.TerminateHookThreads();
        }

        public void SetupHotkeys(IntPtr windowPtr) // TODO No tests here yet as this will change significantly when settings are introduced
        {
            const int MOD_WIN = 0x8;
            const int MOD_CONTROL = 0x2;
            DesktopManager.RegisterHotKeys(windowPtr, (int)HotkeyEvents.Tile, (uint) (MOD_WIN | MOD_CONTROL), 0x54); //T for Tile
            DesktopManager.RegisterHotKeys(windowPtr, (int)HotkeyEvents.Untile, (uint) (MOD_WIN | MOD_CONTROL), 0x55); //U for Untile
        }

        public void UnregisterHotKeys(IntPtr windowPtr) // TODO No tests here yet as this will change significantly when settings are introduced
        {
            DesktopManager.UnregisterHotKeys(windowPtr, 1);
            DesktopManager.UnregisterHotKeys(windowPtr, 2);
        }

        #endregion
    }

    public interface IController
    {
        void SetupHook(IntPtr windowPtr);
        void TerminateHook();
        void SetupHotkeys(IntPtr windowPtr);
        void UnregisterHotKeys(IntPtr windowPtr);
        void SetLayouts(string layout);
        int RegisterWindowMessage(string windowMessage);
        void RefreshAllTags();
    }
}

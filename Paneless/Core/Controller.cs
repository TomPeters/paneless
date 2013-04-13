﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Paneless.Core;
using Paneless.Core.Events;
using Paneless.WinApi;
using Paneless.WinApi.Constants;

namespace Paneless
{
    // Orchestrates Core
    public class Controller : IController
    {
        private readonly int _windowMessage;
        


        private IDesktop Desktop { get; set; }

        public ILayout DefaultLayout { get; set; } // This should probably move to settings

        private IWindowManager WindowManager { get; set; }

        private IDesktopManager DesktopManager { get; set; }

        public Controller(ILayout defaultLayout)
            : this(defaultLayout, new Desktop(), new WindowManager(), new DesktopManager())
        {
        }

        public Controller(ILayout defaultLayout, IDesktop desktop, IWindowManager windowManager, IDesktopManager desktopManager)
        {
            Desktop = desktop;
            DefaultLayout = defaultLayout;
            WindowManager = windowManager;
            DesktopManager = desktopManager;

            SetDefaultLayouts();
            AssignWindows();
            //SetLayouts(DefaultLayout);
        }

        public int RegisterWindowMessage(string windowMessage)
        {
            return DesktopManager.RegisterWindowMessage(windowMessage);
        }

        public void SetLayouts(ILayout layout)
        {
            foreach (IMonitor monitor in Desktop.Monitors)
            {
                monitor.Tag.SetLayout(layout);
            }
        }

        private void SetDefaultLayouts() //TODO this is temporary and should be put into some kind of settings/config
        {
            foreach (IMonitor monitor in Desktop.Monitors)
            {
                ITag newTag = new Tag();
                Desktop.AddTag(newTag);
                monitor.Tag = newTag;
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

        public void SetupHook(IntPtr windowPtr)
        {
            DesktopManager.SetupWindowsHook(windowPtr); // TODO Use return type to check if this succeeded
        }

        public void TerminateHook()
        {
            DesktopManager.TerminateHookThreads();
        }

        public void SetupHotkeys(IntPtr windowPtr)
        {
            const int MOD_WIN = 0x8;
            const int MOD_CONTROL = 0x2;
            DesktopManager.RegisterHotKeys(windowPtr, (int)HotkeyEvents.Tile, (uint) (MOD_WIN | MOD_CONTROL), 0x54); //T for Tile
            DesktopManager.RegisterHotKeys(windowPtr, (int)HotkeyEvents.Untile, (uint) (MOD_WIN | MOD_CONTROL), 0x55); //U for Untile
        }

        public void UnregisterHotKeys(IntPtr windowPtr)
        {
            DesktopManager.UnregisterHotKeys(windowPtr, 1);
            DesktopManager.UnregisterHotKeys(windowPtr, 2);
        }
    }

    public interface IController
    {
        void SetupHook(IntPtr windowPtr);
        void TerminateHook();
        void SetupHotkeys(IntPtr windowPtr);
        void UnregisterHotKeys(IntPtr windowPtr);
        void SetLayouts(ILayout layout);
        ILayout DefaultLayout { get; set; }
        int RegisterWindowMessage(string windowMessage);
    }
}

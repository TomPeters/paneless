using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Paneless.Core;
using Paneless.WinApi;
using Paneless.WinApi.Constants;

namespace Paneless
{
    // Orchestrates Core
    public class Controller : IController
    {
        private readonly int _windowMessage;
        private const string CustomWindowMessage = "PANELESS_7F75020C-34E7-45B4-A5F8-6827F9DB7DE2";

        private IDesktop Desktop { get; set; }

        private ILayout InitialLayout { get; set; }

        private IWindowManager WindowManager { get; set; }

        private IDesktopManager DesktopManager { get; set; }

        public Controller(ILayout initialLayout)
            : this(initialLayout, new Desktop(), new WindowManager(), new DesktopManager())
        {
        }

        public Controller(ILayout initialLayout, IDesktop desktop, IWindowManager windowManager, IDesktopManager desktopManager)
        {
            Desktop = desktop;
            InitialLayout = initialLayout;
            WindowManager = windowManager;
            DesktopManager = desktopManager;

            _windowMessage = DesktopManager.RegisterWindowMessage(CustomWindowMessage);

            SetDefaultLayouts();
            AssignWindows();
            //SetInitialLayouts();
        }

        private void SetInitialLayouts()
        {
            foreach (IMonitor monitor in Desktop.Monitors)
            {
                monitor.Tag.SetLayout(InitialLayout);
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

        public void WndProcEventHandler(Message msg)
        {
            if (msg.Msg == _windowMessage) // We are only interested in messages sent from our hooks
            {
                IntPtr HWnd = msg.WParam;
                IntPtr Message = msg.LParam;
                WindowNotification messageType = (WindowNotification)Message;
                switch (messageType)
                {
                    case(WindowNotification.WM_MOVING):
                        // Trigger event wm_moving with argument HWnd
                        break;
                    case(WindowNotification.WM_SHOWWINDOW):
                        // Trigger event WM_Showwindow with argument HWnd
                        break;
                    default:
                        // Unhandled event - do nothing
                        break;
                }
            }
        }
    }

    public interface IController
    {
        void SetupHook(IntPtr windowPtr);
        void TerminateHook();
        void WndProcEventHandler(Message msg);
    }
}

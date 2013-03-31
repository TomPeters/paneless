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
        private readonly ILayout _initialLayout;
        private readonly IWindowManager _windowManager;
        private readonly IDesktopManager _desktopManager;
        private readonly int _windowMessage;
        private const string CustomWindowMessage = "PANELESS_7F75020C-34E7-45B4-A5F8-6827F9DB7DE2";


        public Controller(ILayout initialLayout)
            : this(initialLayout, new Desktop(), new WindowManager(), new DesktopManager())
        {
        }

        private Controller(ILayout initialLayout, IDesktop desktop, IWindowManager windowManager, IDesktopManager desktopManager)
        {
            Desktop = desktop;
            _initialLayout = initialLayout;
            _windowManager = windowManager;
            _desktopManager = desktopManager;

            

            _windowMessage = _desktopManager.RegisterWindowMessage(CustomWindowMessage);

            SetDefaultLayouts();
            AssignWindows();
            //SetInitialLayouts();
        }

        private void SetInitialLayouts()
        {
            foreach (IMonitor monitor in Desktop.Monitors)
            {
                monitor.Tag.SetLayout(_initialLayout);
            }
        }

        public IDesktop Desktop { get; private set; }

        private void SetDefaultLayouts() //TODO this is temporary and should be put into some kind of settings/config
        {
            ITag tag1 = new Tag();
            ITag tag2 = new Tag();
            Desktop.AddTag(tag1);
            Desktop.AddTag(tag2);

            Desktop.Monitors[0].Tag = tag1;
            Desktop.Monitors[1].Tag = tag2;
        }

        private void AssignWindows() // TODO: This should always be done at start up (is this the same as controller construction?)
        {
            List<IWindow> windows = Desktop.DetectWindows();

            foreach (IWindow window in windows)
            {
                foreach (IMonitor monitor in Desktop.Monitors)
                {
                    if (window.Screen.Equals(monitor.Screen))
                    {
                        monitor.AddWindow(window);
                    }
                }
            }
        }

        public void SetupHook(IntPtr windowPtr)
        {
            _desktopManager.SetupWindowsHook(windowPtr); // TODO Use return type to check if this succeeded
        }

        public void TerminateHook()
        {
            _desktopManager.TerminateHookThreads();
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
        IDesktop Desktop { get; }
        void SetupHook(IntPtr windowPtr);
        void TerminateHook();
        void WndProcEventHandler(Message msg);
    }
}

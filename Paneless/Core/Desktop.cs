using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using WinApi.Interface;

namespace Paneless.Core
{
    //Top level class that orchestrates the application
    public class Desktop : IDesktop
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<IMonitor> _monitors;
        private List<IWindow> _windows;
        private readonly List<ITag> _tags; 
        private readonly WindowsEnumProcess _windowsEnumCallBack;

        public Desktop(IDesktopManager desktopManager, IWindowManager windowManager)
        {
            DesktopManager = desktopManager;
            WindowManager = windowManager;
            _windowsEnumCallBack = AddDetectedWindow;
            _tags = new List<ITag>();
            PopulateMonitors();
        }

        private IDesktopManager DesktopManager { get; set; }
        private IWindowManager WindowManager { get; set; }

        private void PopulateMonitors()
        {
            _monitors = new List<IMonitor>();
            List<Screen> screens = Screen.AllScreens.ToList();
            foreach (Screen screen in screens)
            {
                _monitors.Add(new Monitor {Screen = screen});
            }
        }

        public List<IMonitor> Monitors
        {
            get { return _monitors; }
        }

        public List<ITag> Tags { get { return _tags; } }

        public void AddTag(ITag tag)
        {
            _tags.Add(tag);
        }

        public List<IWindow> DetectWindows()
        {
            _windows = new List<IWindow>();
            DesktopManager.EnumWindows(_windowsEnumCallBack);
            return _windows;
        }

        private bool AddDetectedWindow(int windowsPtr, int lParam)
        {
            IWindow window = new Window((IntPtr) windowsPtr, WindowManager);
            Logger.Debug("Window Detected: " + window.Name);
            if (window.IsTileable())
            {
                Logger.Debug("Tileable Window " + window.Name + " added to Desktop");
                _windows.Add(window);
            }
            return true;
        }
    }

    public interface IDesktop
    {
        List<IMonitor> Monitors { get; }
        List<ITag> Tags { get; } 
        List<IWindow> DetectWindows();
        void AddTag(ITag tag);
    }
}

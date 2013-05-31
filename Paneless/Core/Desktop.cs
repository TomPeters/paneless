using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Paneless.Core.Layouts;
using WinApi.Interface;

namespace Paneless.Core
{
    public class Desktop : IDesktop
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<IMonitor> _monitors;
        private List<IWindow> _windows;
        private readonly List<ITag> _tags; 
        private readonly WindowsEnumProcess _windowsEnumCallBack;

        public Desktop(IDesktopManager desktopManager, IWindowManager windowManager
            , ILayoutFactory layoutFactory, IWindowFactory windowFactory) // Layout factory probably shouldn't even be a property here, and neither should window factory
        {
            DesktopManager = desktopManager;
            WindowManager = windowManager;
            LayoutFactory = layoutFactory;
            WindowFactory = windowFactory;
            _windowsEnumCallBack = AddDetectedWindow;
            _tags = new List<ITag>();
            PopulateMonitors();
        }

        private IDesktopManager DesktopManager { get; set; }
        private IWindowManager WindowManager { get; set; }
        public ILayoutFactory LayoutFactory { get; set; } // This shouldn't need to be public
        private IWindowFactory WindowFactory { get; set; }

        private void PopulateMonitors()
        {
            _monitors = new List<IMonitor>();
            List<Screen> screens = Screen.AllScreens.ToList();
            foreach (Screen screen in screens)
            {
                _monitors.Add(new Monitor {Screen = screen});
            }
        }

        public IEnumerable<IMonitor> Monitors
        {
            get { return _monitors; }
        }

        public IEnumerable<ITag> Tags { get { return _tags; } }

        public void AddTag(ITag tag)
        {
            _tags.Add(tag);
        }

        public bool IsManagingWindow(IWindow window)
        {
            return _windows.All(w => w.Equals(window));
        }

        public IEnumerable<IWindow> DetectWindows()
        {
            _windows = new List<IWindow>();
            DesktopManager.EnumWindows(_windowsEnumCallBack);
            return _windows;
        }

        // This logic probably shouldn't be here
        private bool AddDetectedWindow(int windowsPtr, int lParam)
        {
            IWindow window = WindowFactory.CreateWindow((IntPtr) windowsPtr);
            Logger.Debug("Window Detected: " + window.Name);
            if (window.IsTileable() && window.IsVisible())
            {
                Logger.Info("Tileable Window " + window.Name + " added to Desktop");
                _windows.Add(window);
            }
            return true;
        }
    }

    public interface IDesktop
    {
        IEnumerable<IMonitor> Monitors { get; }
        IEnumerable<ITag> Tags { get; }
        ILayoutFactory LayoutFactory { get; }
        IEnumerable<IWindow> DetectWindows();
        void AddTag(ITag tag);
        bool IsManagingWindow(IWindow window);
    }
}

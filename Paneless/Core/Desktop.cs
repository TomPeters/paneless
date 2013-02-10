using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Paneless.WinApi;

namespace Paneless.Core
{
    public class Desktop
    {
        private List<Screen> _screens;
        private List<IWindow> _windows;
        private readonly WindowsEnumProcess _windowsEnumCallBack;

        public Desktop()
            : this(new DesktopManager(), new WindowManager())
        {
        }

        public Desktop(IDesktopManager desktopManager, IWindowManager windowManager)
        {
            DesktopManager = desktopManager;
            WindowManager = windowManager;
            _windowsEnumCallBack = AddWindow;
            _windows = new List<IWindow>();
        }

        private IDesktopManager DesktopManager { get; set; }
        private IWindowManager WindowManager { get; set; }

        public void PopulateScreens()
        {
            _screens = Screen.AllScreens.ToList();
        }

        public List<Screen> Screens
        {
            get { return _screens; }
        }

        public void PopulateWindows()
        {
            DesktopManager.EnumWindows(_windowsEnumCallBack);
        }

        private bool AddWindow(int windowsPtr, int lParam)
        {
            IWindow window = new Window((IntPtr) windowsPtr, WindowManager);
            if (window.IsTileable())
            {
                _windows.Add(window);
            }
            return true;
        }

        public IEnumerable<IWindow> Windows
        {
            get { return _windows; }
        } 
    }
}

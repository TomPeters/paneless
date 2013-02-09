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
        private List<Window> _windows;
        private readonly WindowsEnumProcess _windowsEnumCallBack;

        public Desktop()
            : this(new DesktopManager())
        {
        }

        public Desktop(IDesktopManager desktopManager)
        {
            DesktopManager = desktopManager;
            _windowsEnumCallBack = AddWindow;
            _windows = new List<Window>();
        }

        private IDesktopManager DesktopManager { get; set; }

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
            _windows.Add(new Window((IntPtr)windowsPtr));
            return true;
        }

        public List<Window> Windows
        {
            get { return _windows; }
        } 
    }
}

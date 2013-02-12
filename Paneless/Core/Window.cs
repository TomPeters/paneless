using System;
using System.Windows.Forms;
using Paneless.WinApi;

namespace Paneless.Core
{
    public class Window : IWindow
    {
        public Window(IntPtr windowPtr)
            : this(windowPtr, new WindowManager())
        {
        }

        public Window(string windowName)
            : this(windowName, new WindowManager())
        {
        }

        public Window(IntPtr windowPtr, IWindowManager windowManager)
        {
            Wmgr = windowManager;
            WindowPtr = windowPtr;
            DetectScreen();
        }

        public Window(string windowname, IWindowManager windowManager)
        {
            Wmgr = windowManager;
            WindowPtr = Wmgr.GetPtr(windowname);
            DetectScreen();
        }

        private IWindowManager Wmgr { get; set; }

        private IntPtr WindowPtr { get; set; }

        public string Name
        {
            get { return Wmgr.GetTitle(WindowPtr); }
        }

        public string ClassName
        {
            get { return Wmgr.GetClassName(WindowPtr); }
        }

        public Rectangle Location
        {
            get { return new Rectangle(Wmgr.GetLocation(WindowPtr)); }
        }

        public Screen Screen { get; set; }

        public ShowState State
        {
            get { return Wmgr.GetShowState(WindowPtr); }
        }

        public void DetectScreen()
        {
            Screen = Screen.FromHandle(WindowPtr);
        }

        public void SetLocation(Rectangle location)
        {
            Wmgr.SetLocationUnchangedOrder(WindowPtr, location.GetRect());
        }

        public ExtendedWindowStyleFlags ExtendedWindowStyleFlags
        {
            get { return Wmgr.GetExtendedStyle(WindowPtr); }
        }

        // Returns true if this window should be handled by Paneless
        public bool IsTileable()
        {
            return IsVisible() && (IsAppWindow() || (HasValidName() && !IsToolWindow()));
        }

        private bool IsAppWindow()
        {
            if (ExtendedWindowStyleFlags.HasFlag(ExtendedWindowStyleFlags.WS_EX_APPWINDOW))
            {
                return true;
            }
            return false;
        }

        private bool IsToolWindow()
        {
            if (ExtendedWindowStyleFlags.HasFlag(ExtendedWindowStyleFlags.WS_EX_TOOLWINDOW))
            {
                return true;
            }
            return false;
        }

        private bool HasValidName() //TODO This isn't a great way to filter windows (but works ok) - look into alternate methods
        {
            if (Name != "" && ClassName != "")
            {
                return true;
            }
            return false;
        }

        private bool IsVisible()
        {
            return Wmgr.IsWindowVisible(WindowPtr);
        }
    }

    public interface IWindow
    {
        string Name { get; }
        Rectangle Location { get; }
        string ClassName { get; }
        ShowState State { get; }
        Screen Screen { get; set; }
        void DetectScreen();
        void SetLocation(Rectangle location);
        ExtendedWindowStyleFlags ExtendedWindowStyleFlags { get; }
        bool IsTileable();
    }
}

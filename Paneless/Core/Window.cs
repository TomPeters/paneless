using System;
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
        }

        public Window(string windowname, IWindowManager windowManager)
        {
            Wmgr = windowManager;
            WindowPtr = Wmgr.GetPtr(windowname);
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

        public WindowLocation Location
        {
            get { return new WindowLocation(Wmgr.GetLocation(WindowPtr)); }
        }

        public void SetLocation(WindowLocation location)
        {
            Wmgr.SetLocationUnchangedOrder(WindowPtr, location.GetRect());
        }

        public ShowState State
        {
            get { return Wmgr.GetShowState(WindowPtr); }
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

        private bool HasValidName() //TODO This isn't a great way to filter windows - look into alternate methods
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
        WindowLocation Location { get; }
        string ClassName { get; }
        ShowState State { get; }
        void SetLocation(WindowLocation location);
        ExtendedWindowStyleFlags ExtendedWindowStyleFlags { get; }
        bool IsTileable();
    }
}

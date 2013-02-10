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

        public void SetLocation(WindowLocation location)
        {
            Wmgr.SetLocationUnchangedOrder(WindowPtr, location.GetRect());
        }

        public WindowLocation Location
        {
            get { return new WindowLocation(Wmgr.GetLocation(WindowPtr)); }
        }

        public ShowState State
        {
            get
            {
                WINDOWPLACEMENT placement = Wmgr.GetWindowPlacement(WindowPtr);
                return ResultState(placement);
            }
        }

        private static ShowState ResultState(WINDOWPLACEMENT placement)
        {
            string stateString = WindowStatus.StatusMap[placement.showCmd];
            ShowState resultState = (ShowState) Enum.Parse(typeof (ShowState), stateString);
            return resultState;
        }

        // Returns true if this window should be handled by Paneless
        public bool IsTileable()
        {
            ShowState curState = State;
            if (curState == ShowState.SW_SHOWMAXIMIZED || curState == ShowState.SW_SHOWMINIMIZED)
            {
                return true;
            }
            return false;
        }
    }

    public interface IWindow
    {
        string Name { get; }
        WindowLocation Location { get; }
        string ClassName { get; }
        void SetLocation(WindowLocation location);
        ShowState State { get; }
        bool IsTileable();
    }
}

using System;
using System.Windows.Forms;
using WinApi.Interface;
using WinApi.Interface.Constants;

namespace Paneless.Core
{
    public class Window : IWindow
    {
        public Window(string windowName, IWindowManager windowManager)
            : this(windowManager.GetPtr(windowName), windowManager)
        {
        }

        public Window(IntPtr windowPtr, IWindowManager windowManager)
        {
            Wmgr = windowManager;
            WindowPtr = windowPtr;
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

        public Screen Screen { get; private set; }

        private void DetectScreen()
        {
            Screen = Screen.FromHandle(WindowPtr);
        }

        public void SetLocation(Rectangle location)
        {
            SetShowState(ShowState.SW_RESTORE);
            Wmgr.SetLocationUnchangedOrder(WindowPtr, location.GetRect());
        }

        public bool IsTileable()
        {
            return Wmgr.IsTileable(WindowPtr);
        }

        public bool IsVisible()
        {
            return Wmgr.IsVisible(WindowPtr);
        }

        private void SetShowState(ShowState showState)
        {
            Wmgr.SetWindowShowState(WindowPtr, showState);
        }

        public bool Equals(IWindow other)
        {
            if (!(other is Window)) return false;
            return ((Window)other).WindowPtr == WindowPtr;
        }
    }

    public interface IWindow : IEquatable<IWindow>
    {
        string Name { get; }
        Rectangle Location { get; }
        string ClassName { get; }
        Screen Screen { get; }
        void SetLocation(Rectangle location);
        bool IsTileable();
        bool IsVisible();
    }
}

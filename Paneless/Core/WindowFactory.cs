using System;
using WinApi.Interface;

namespace Paneless.Core
{
    public class WindowFactory : IWindowFactory
    {
        private IWindowManager WindowManager { get; set; }

        public WindowFactory(IWindowManager windowManager)
        {
            WindowManager = windowManager;
        }

        public IWindow CreateWindow(IntPtr windowPtr)
        {
            return new Window(windowPtr, WindowManager);
        }
    }

    public interface IWindowFactory
    {
        IWindow CreateWindow(IntPtr windowPtr);
    }
}

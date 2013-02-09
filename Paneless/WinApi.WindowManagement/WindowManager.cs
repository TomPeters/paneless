using System;
using System.Text;

namespace Paneless.WinApi
{
    public class WindowManager : IWindowManager
    {
        public IntPtr GetPtr(string windowName)
        {
            return (IntPtr)WinApi.FindWindow(null, windowName);
        }

        public string GetTitle(IntPtr windowPtr)
        {
            int titleLength = WinApi.GetWindowTextLength(windowPtr);
            StringBuilder titleStringBuilder = new StringBuilder(titleLength);
            WinApi.GetWindowText(windowPtr, titleStringBuilder, titleLength+1);
            return titleStringBuilder.ToString();
        }

        public string GetClassName(IntPtr windowPtr)
        {
            const int classNameLength = 256; // Maximum possible length is 256 chars http://msdn.microsoft.com/en-us/library/windows/desktop/ms633576(v=vs.85).aspx
            StringBuilder titleStringBuilder = new StringBuilder(classNameLength);
            WinApi.GetClassName(windowPtr, titleStringBuilder, classNameLength);
            return titleStringBuilder.ToString();
        }

        public RECT GetLocation(IntPtr windowPtr)
        {
            RECT rect = new RECT();
            WinApi.GetWindowRect(windowPtr, rect);
            return rect;
        }

        public void SetLocationUnchangedOrder(IntPtr windowPtr, RECT rect)
        {
            SetLocation(windowPtr, IntPtr.Zero, rect, (uint) (PositioningFlags.SWP_NOZORDER));
        }

        public void SetLocation(IntPtr windowPtr, IntPtr windowInsertAfter, RECT rect, uint positioningFlags)
        {
            WinApi.SetWindowPos(windowPtr, windowInsertAfter, rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top, positioningFlags);
        }
    }

    public interface IWindowManager
    {
        IntPtr GetPtr(string windowName);
        string GetTitle(IntPtr windowPtr);
        string GetClassName(IntPtr windowPtr);
        RECT GetLocation(IntPtr windowPtr);
        void SetLocationUnchangedOrder(IntPtr windowPtr, RECT rect);
        void SetLocation(IntPtr windowPtr, IntPtr windowInsertAfter, RECT rect, uint positioningFlags);
    }
}

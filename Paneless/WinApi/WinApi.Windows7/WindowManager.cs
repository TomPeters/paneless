using System;
using System.Runtime.InteropServices;
using System.Text;
using WinApi.Interface;
using WinApi.Interface.Constants;
using WinApi.Interface.Types;
using WinApi.Windows7.Constants;

namespace WinApi.Windows7
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

        public ShowState GetShowState(IntPtr windowPtr)
        {
            WINDOWPLACEMENT placement = GetWindowPlacement(windowPtr);
            string stateString = WindowStatusLookup.StatusMap[placement.showCmd];
            ShowState resultState = (ShowState)Enum.Parse(typeof(ShowState), stateString);
            return resultState;
        }

        public WINDOWPLACEMENT GetWindowPlacement(IntPtr windowPtr)
        {
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            WinApi.GetWindowPlacement(windowPtr, out placement);
            return placement;
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

        private void SetLocation(IntPtr windowPtr, IntPtr windowInsertAfter, RECT rect, uint positioningFlags)
        {
            WinApi.SetWindowPos(windowPtr, windowInsertAfter, rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top, positioningFlags);
        }

        public bool IsWindowVisible(IntPtr windowPtr)
        {
            return WinApi.IsWindowVisible(windowPtr);
        }

        public ExtendedWindowStyleFlags GetExtendedStyle(IntPtr windowPtr)
        {
            return (ExtendedWindowStyleFlags)WinApi.GetWindowLong(windowPtr, GetWindowLongNIndex.GWL_EXSTYLE);
        }
    }
}

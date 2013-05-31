using System;
using WinApi.Interface.Constants;
using WinApi.Interface.Types;

namespace WinApi.Interface
{
    /// <summary>
    /// Interface representing interaction with a window that can be tiled.
    /// Responsible for changing the state of tileable window 
    /// or retrieving information about a potentially tileable window
    /// </summary>
    public interface IWindowManager
    {
        IntPtr GetPtr(string windowName);
        string GetTitle(IntPtr windowPtr);
        string GetClassName(IntPtr windowPtr);
        RECT GetLocation(IntPtr windowPtr);
        void SetLocationUnchangedOrder(IntPtr windowPtr, RECT rect);
        ShowState GetShowState(IntPtr windowPtr);
        WINDOWPLACEMENT GetWindowPlacement(IntPtr windowPtr);
        ExtendedWindowStyleFlags GetExtendedStyle(IntPtr windowPtr);
        void SetWindowShowState(IntPtr windowPtr, ShowState showState);
        bool IsTileable(IntPtr windowPtr);
        bool IsVisible(IntPtr windowPtr);
    }
}

using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paneless.WinApi;
using EasyAssertions;

namespace WinApi.IntergrationTests
{
    [TestClass]
    public class NotepadIntegrationTests
    {
        private const string NotepadTitle = "Untitled - Notepad";
        private const string NotepadProcess = "Notepad.exe";
        private const string NotepadClass = "Notepad";
        private Process _process;
        private IWindowManager _wm;
        private IntPtr _notepadPtr;

        [TestInitialize]
        public void Setup()
        {
            _process = Process.Start(NotepadProcess);
            Thread.Sleep(1000);
            _wm = new WindowManager();
            _notepadPtr = _wm.GetPtr(NotepadTitle);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _process.CloseMainWindow();
            _process.Close();
        }

        [TestMethod]
        public void GetPtrFromWindowString()
        {
            IntPtr notepadPtr = _wm.GetPtr(NotepadTitle);
            notepadPtr.ShouldNotBe(IntPtr.Zero);
        }

        [TestMethod]
        public void GetTitle()
        {
            string title = _wm.GetTitle(_notepadPtr);
            title.ShouldBe(NotepadTitle);
        }

        [TestMethod]
        public void GetClassName()
        {
            string className = _wm.GetClassName(_notepadPtr);
            className.ShouldBe(NotepadClass);
        }

        [TestMethod]
        public void GetShowState()
        {
            ShowState showState = _wm.GetShowState(_notepadPtr);
            showState.ShouldBe(ShowState.SW_SHOWNORMAL);
        }

        [TestMethod]
        public void GetWindowPlacement()
        {
            WINDOWPLACEMENT placement = _wm.GetWindowPlacement(_notepadPtr);
            // Only need to assert show cmd as this is the only thing we are using
            placement.showCmd.ShouldBe(1);
        }

        [TestMethod]
        public void GetLocation()
        {
            RECT location = _wm.GetLocation(_notepadPtr);
            location.Right.ShouldBeGreaterThan(location.Left);
            location.Bottom.ShouldBeGreaterThan(location.Top);
        }

        [TestMethod]
        public void SetLocationUnchangedOrder()
        {
            RECT location = _wm.GetLocation(_notepadPtr);
            location.Left = location.Left + 1;
            _wm.SetLocationUnchangedOrder(_notepadPtr, location);

            // Check new location to confirm that it has moved
            RECT newLocation = _wm.GetLocation(_notepadPtr);
            newLocation.Left.ShouldBe(location.Left);
        }

        [TestMethod]
        public void IsWindowVisible()
        {
            bool visibility = _wm.IsWindowVisible(_notepadPtr);
            visibility.ShouldBe(true);
        }

        [TestMethod]
        public void GetExtendedStyle()
        {
            ExtendedWindowStyleFlags extendedStyle = _wm.GetExtendedStyle(_notepadPtr);
            extendedStyle.HasFlag(ExtendedWindowStyleFlags.WS_EX_LEFT).ShouldBe(true);
        }
    }
}

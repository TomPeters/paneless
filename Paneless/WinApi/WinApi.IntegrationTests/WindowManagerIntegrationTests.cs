using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyAssertions;
using WinApi.Interface;
using WinApi.Interface.Constants;
using WinApi.Interface.Types;
using WinApi.Windows7;

namespace WinApi.IntergrationTests
{
    [TestClass]
    public abstract class WindowManagerIntegrationTests
    {
        protected string ApplicationTitle;
        protected string ApplicationProcess;
        protected string ApplicationClass;
        private Process _process;
        private IWindowManager _wm;
        private IntPtr _applicationPtr;

        protected abstract IWindowManager WindowManager { get; }

        [TestInitialize]
        public void Setup()
        {
            _process = Process.Start(ApplicationProcess);
            Thread.Sleep(1000);
            _wm = WindowManager;
            _applicationPtr = _wm.GetPtr(ApplicationTitle);
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
            IntPtr notepadPtr = _wm.GetPtr(ApplicationTitle);
            notepadPtr.ShouldNotBe(IntPtr.Zero);
        }

        [TestMethod]
        public void GetTitle()
        {
            string title = _wm.GetTitle(_applicationPtr);
            title.ShouldBe(ApplicationTitle);
        }

        [TestMethod]
        public void GetClassName()
        {
            string className = _wm.GetClassName(_applicationPtr);
            className.ShouldBe(ApplicationClass);
        }

        [TestMethod]
        public void GetShowState()
        {
            ShowState showState = _wm.GetShowState(_applicationPtr);
            showState.ShouldBe(ShowState.SW_SHOWNORMAL);
        }

        [TestMethod]
        public void GetWindowPlacement()
        {
            WINDOWPLACEMENT placement = _wm.GetWindowPlacement(_applicationPtr);
            // Only need to assert show cmd as this is the only thing we are using
            placement.showCmd.ShouldBe(1);
        }

        [TestMethod]
        public void GetLocation()
        {
            RECT location = _wm.GetLocation(_applicationPtr);
            location.Right.ShouldBeGreaterThan(location.Left);
            location.Bottom.ShouldBeGreaterThan(location.Top);
        }

        [TestMethod]
        public void SetLocationUnchangedOrder()
        {
            RECT location = _wm.GetLocation(_applicationPtr);
            location.Left = location.Left + 1;
            _wm.SetLocationUnchangedOrder(_applicationPtr, location);

            // Check new location to confirm that it has moved
            RECT newLocation = _wm.GetLocation(_applicationPtr);
            newLocation.Left.ShouldBe(location.Left);
        }

        [TestMethod]
        public void IsWindowVisible()
        {
            bool visibility = _wm.IsWindowVisible(_applicationPtr);
            visibility.ShouldBe(true);
        }

        [TestMethod]
        public void GetExtendedStyle()
        {
            ExtendedWindowStyleFlags extendedStyle = _wm.GetExtendedStyle(_applicationPtr);
            extendedStyle.HasFlag(ExtendedWindowStyleFlags.WS_EX_LEFT).ShouldBe(true);
        }
    }

    [TestClass]
    public abstract class Windows7WindowManagerIntegrationTests : WindowManagerIntegrationTests
    {
        protected override IWindowManager WindowManager
        {
            get { return new WindowManager(); }
        }
    }

    [TestClass]
    public class NotepadWindowManagerIntegrationTests : Windows7WindowManagerIntegrationTests
    {
        public NotepadWindowManagerIntegrationTests() 
        {
            ApplicationTitle = "Untitled - Notepad";
            ApplicationProcess = "Notepad.exe";
            ApplicationClass = "Notepad";
        }
    }

    [TestClass]
    public class CalculatorWindowManagerIntegrationTests : Windows7WindowManagerIntegrationTests
    {
        public CalculatorWindowManagerIntegrationTests()
        {
            ApplicationTitle = "Calculator";
            ApplicationProcess = "calc.exe";
            ApplicationClass = "CalcFrame";
        }
    }
}

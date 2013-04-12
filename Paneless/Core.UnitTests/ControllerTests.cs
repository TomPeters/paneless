using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paneless.WinApi;

namespace Paneless.Core.UnitTests
{
    [TestClass]
    public class ControllerTests
    {
        private Controller _sut;
        private Mock<ILayout> _initialLayoutMock;
        private Mock<IDesktop> _desktopMock;
        private Mock<IDesktopManager> _dmMock;
        private Mock<IWindowManager> _wmMock;
        private List<IMonitor> _monitors;
        private List<IWindow> _windows;

        [TestInitialize]
        public void Setup()
        {
            _initialLayoutMock = new Mock<ILayout>();
            _desktopMock = new Mock<IDesktop>();
            _monitors = new List<IMonitor>();
            _windows = new List<IWindow>();
            _wmMock = new Mock<IWindowManager>();
            _dmMock = new Mock<IDesktopManager>();
        }

        private void ConstructSut()
        {
            _desktopMock.Setup(d => d.Monitors).Returns(_monitors);
            _desktopMock.Setup(d => d.DetectWindows()).Returns(_windows);
            _sut = new Controller(_initialLayoutMock.Object, _desktopMock.Object, _wmMock.Object, _dmMock.Object);
        }

        [TestMethod]
        public void Constructor_CorrectDependencyCallsAreMade()
        {
            ConstructSut();
            _desktopMock.Verify(d => d.Monitors, Times.Once());
            _dmMock.Verify(dm => dm.RegisterWindowMessage("PANELESS_7F75020C-34E7-45B4-A5F8-6827F9DB7DE2"), Times.Once());
            _desktopMock.Verify(d => d.DetectWindows(), Times.Once());
        }

        [TestMethod]
        public void Constructor_MonitorsHaveTags()
        {
            Mock<IMonitor> mockMonitor1 = new Mock<IMonitor>();
            Mock<IMonitor> mockMonitor2 = new Mock<IMonitor>();
            _monitors.Add(mockMonitor1.Object);
            _monitors.Add(mockMonitor2.Object);

            ConstructSut();

            mockMonitor1.VerifySet(m => m.Tag = It.IsAny<ITag>(), Times.Once());
            mockMonitor2.VerifySet(m => m.Tag = It.IsAny<ITag>(), Times.Once());
            _desktopMock.Verify(d => d.AddTag(It.IsAny<ITag>()), Times.Exactly(2));
        }

        [TestMethod]
        public void Constructor_TwoWindowsTwoMonitors_WindowsAreCorrectlyAssigned()
        {
            Mock<IWindow> window1 = new Mock<IWindow>();
            Mock<IWindow> window2 = new Mock<IWindow>();
            _windows.Add(window1.Object);
            _windows.Add(window2.Object);

            Mock<IMonitor> monitor1 = new Mock<IMonitor>();
            Mock<IMonitor> monitor2 = new Mock<IMonitor>();
            monitor1.Setup(m => m.IsInSameScreen(It.IsAny<IWindow>())).Returns((IWindow w) => w == window2.Object);
            monitor2.Setup(m => m.IsInSameScreen(It.IsAny<IWindow>())).Returns((IWindow w) => w == window1.Object);
            _monitors.Add(monitor1.Object);
            _monitors.Add(monitor2.Object);

            ConstructSut();

            monitor1.Verify(m => m.AddWindow(window2.Object), Times.Once());
            monitor2.Verify(m => m.AddWindow(window1.Object), Times.Once());
        }

        [TestMethod]
        public void Constructor_TwoWindowsTwoMonitorsReverse_WindowsAreCorrectlyAssigned()
        {
            Mock<IWindow> window1 = new Mock<IWindow>();
            Mock<IWindow> window2 = new Mock<IWindow>();
            _windows.Add(window1.Object);
            _windows.Add(window2.Object);

            Mock<IMonitor> monitor1 = new Mock<IMonitor>();
            Mock<IMonitor> monitor2 = new Mock<IMonitor>();
            monitor1.Setup(m => m.IsInSameScreen(It.IsAny<IWindow>())).Returns((IWindow w) => w == window1.Object);
            monitor2.Setup(m => m.IsInSameScreen(It.IsAny<IWindow>())).Returns((IWindow w) => w == window2.Object);
            _monitors.Add(monitor1.Object);
            _monitors.Add(monitor2.Object);

            ConstructSut();

            monitor1.Verify(m => m.AddWindow(window1.Object), Times.Once());
            monitor2.Verify(m => m.AddWindow(window2.Object), Times.Once());
        }

        [TestMethod]
        public void SetupHook_CalledWithAPtr_DependencyMethodCalledWithSamePtr()
        {
            ConstructSut();
            IntPtr ptr = new IntPtr(500);

            _sut.SetupHook(ptr);

            _dmMock.Verify(dm => dm.SetupWindowsHook(It.Is<IntPtr>(p => p == ptr)), Times.Once());
        }

        [TestMethod]
        public void TerminateHook_DependencyMethodCalled()
        {
            ConstructSut();

            _sut.TerminateHook();

            _dmMock.Verify(dm => dm.TerminateHookThreads(), Times.Once());
        }
    }
}

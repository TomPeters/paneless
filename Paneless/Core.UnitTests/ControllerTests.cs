using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paneless.Core.Layouts;
using WinApi.Interface;

namespace Paneless.Core.UnitTests
{
    [TestClass]
    public class ControllerTests
    {
        private Controller _sut;
        private Mock<ILayoutFactory> _layoutFactoryMock;
        private Mock<IDesktop> _desktopMock;
        private Mock<IDesktopManager> _dmMock;
        private Mock<IWindowManager> _wmMock;
        private List<IMonitor> _monitors;
        private List<IWindow> _windows;
        private Mock<ILayout> _layout;

        [TestInitialize]
        public void Setup()
        {
            _layoutFactoryMock = new Mock<ILayoutFactory>();
            _layout = new Mock<ILayout>();
            _layoutFactoryMock.Setup(lf => lf.CreateLayout(It.IsAny<string>())).Returns(_layout.Object);
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
            _sut = new Controller(_layoutFactoryMock.Object, _desktopMock.Object, _dmMock.Object, _wmMock.Object);
        }

        [TestMethod]
        public void Constructor_CorrectDependencyCallsAreMade()
        {
            ConstructSut();
            _desktopMock.Verify(d => d.Monitors, Times.Once());
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

        [TestMethod]
        public void RegisterWindowMessage_DependencyMethodCalled()
        {
            ConstructSut();

            const string windowMessage = "TestMessage";
            _sut.RegisterWindowMessage(windowMessage);

            _dmMock.Verify(dm => dm.RegisterWindowMessage(windowMessage), Times.Once());
        }

        [TestMethod]
        public void Constructor_SetLayout_WindowsAreAssigned()
        {
            Mock<IWindow> window1 = new Mock<IWindow>();
            Mock<IWindow> window2 = new Mock<IWindow>();
            _windows.Add(window1.Object);
            _windows.Add(window2.Object);

            Mock<IMonitor> monitor1 = new Mock<IMonitor>();
            Mock<IMonitor> monitor2 = new Mock<IMonitor>();
            monitor1.Setup(m => m.IsInSameScreen(It.IsAny<IWindow>())).Returns((IWindow w) => w == window1.Object);
            monitor2.Setup(m => m.IsInSameScreen(It.IsAny<IWindow>())).Returns((IWindow w) => w == window2.Object);
            Mock<ITag> tag1 = new Mock<ITag>();
            Mock<ITag> tag2 = new Mock<ITag>();
            monitor1.Setup(m => m.Tag).Returns(tag1.Object);
            monitor2.Setup(m => m.Tag).Returns(tag2.Object);
            _monitors.Add(monitor1.Object);
            _monitors.Add(monitor2.Object);

            ConstructSut();

            _sut.SetLayouts("");

            monitor1.Verify(m => m.AddWindow(window1.Object), Times.Once());
            monitor2.Verify(m => m.AddWindow(window2.Object), Times.Once());

            tag1.Verify(t => t.SetLayout(It.Is<ILayout>(l => l == _layout.Object)), Times.Once());
            tag2.Verify(t => t.SetLayout(It.Is<ILayout>(l => l == _layout.Object)), Times.Once());
        }
    }
}

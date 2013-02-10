using System;
using System.Collections.Generic;
using System.Linq;
using EasyAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paneless.WinApi;

namespace Paneless.Core.UnitTests
{
    [TestClass]
    public class DesktopTests
    {
        private Mock<IDesktopManager> _mockDesktopManager;
        private Desktop _sut;
        private Mock<IWindowManager> _mockWindowManager;

        [TestInitialize]
        public void Setup()
        {
            _mockDesktopManager = new Mock<IDesktopManager>();
            _mockWindowManager = new Mock<IWindowManager>();
            _sut = new Desktop(_mockDesktopManager.Object, _mockWindowManager.Object);
        }

        [TestMethod]
        public void TestPopulateScrens()
        {
            Desktop sut = new Desktop();
            sut.PopulateScreens();
            sut.Screens.ShouldNotBeNull();
        }

        [TestMethod]
        public void TestPopulateWindows()
        {
            const int windowPtr = 5;
            _mockDesktopManager.Setup(mgr => mgr.EnumWindows(It.IsAny<WindowsEnumProcess>()))
                              .Callback<WindowsEnumProcess>(cb => cb(windowPtr, 0));
            _mockWindowManager.Setup(mgr => mgr.GetTitle(It.IsAny<IntPtr>())).Returns((IntPtr ptr) => "Name");
            _sut.PopulateWindows();
            List<IWindow> windows = _sut.Windows;
            windows.Count().ShouldBe(1);
        }
    }
}

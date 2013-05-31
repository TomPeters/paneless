using System;
using System.Collections.Generic;
using System.Linq;
using EasyAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paneless.Core.Layouts;
using WinApi.Interface;

namespace Paneless.Core.UnitTests
{
    [TestClass]
    public class DesktopTests
    {
        private Mock<IDesktopManager> _mockDesktopManager;
        private Desktop _sut;
        private Mock<IWindowManager> _mockWindowManager;
        private Mock<ILayoutFactory> _mockLayoutFactory;
        private Mock<IWindowFactory> _mockWindowFactory;

        [TestInitialize]
        public void Setup()
        {
            _mockDesktopManager = new Mock<IDesktopManager>();
            _mockWindowManager = new Mock<IWindowManager>();
            _mockLayoutFactory = new Mock<ILayoutFactory>();
            _mockWindowFactory = new Mock<IWindowFactory>();
            _sut = new Desktop(_mockDesktopManager.Object, _mockWindowManager.Object, _mockLayoutFactory.Object, _mockWindowFactory.Object);
        }

        [TestMethod]
        public void AddAndGetTagsTest()
        {
            int tagCount = _sut.Tags.Count();

            _sut.AddTag((new Mock<ITag>()).Object);

            _sut.Tags.Count().ShouldBe(tagCount + 1);
        }

        [TestMethod]
        public void TestDetectWindows()
        {
            _mockWindowFactory.Setup(wf => wf.CreateWindow(It.IsAny<IntPtr>())).Returns(Mock.Of<IWindow>(w => w.IsTileable() == true && w.IsVisible() == true));
            const int windowPtr = 5;
            _mockDesktopManager.Setup(mgr => mgr.EnumWindows(It.IsAny<WindowsEnumProcess>()))
                              .Callback<WindowsEnumProcess>(cb => cb(windowPtr, 0));
            IEnumerable<IWindow> windows = _sut.DetectWindows();
            windows.Count().ShouldBe(1);
        }
    }
}

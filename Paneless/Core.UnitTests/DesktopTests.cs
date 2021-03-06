﻿using System;
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
        public void AddAndGetTagsTest()
        {
            int tagCount = _sut.Tags.Count();

            _sut.AddTag((new Mock<ITag>()).Object);

            _sut.Tags.Count().ShouldBe(tagCount + 1);
        }

        [TestMethod]
        public void TestDetectWindows()
        {
            const int windowPtr = 5;
            _mockDesktopManager.Setup(mgr => mgr.EnumWindows(It.IsAny<WindowsEnumProcess>()))
                              .Callback<WindowsEnumProcess>(cb => cb(windowPtr, 0));
            _mockWindowManager.Setup(mgr => mgr.IsWindowVisible(It.IsAny<IntPtr>())).Returns(true);
            IEnumerable<IWindow> windows = _sut.DetectWindows();
            windows.Count().ShouldBe(1);
        }
    }
}

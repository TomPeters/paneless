﻿using System;
using EasyAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paneless.WinApi;

namespace Paneless.Core.UnitTests
{
    [TestClass]
    public class WindowTests
    {
        private IntPtr _windowPtr;
        private Mock<IWindowManager> _mockWindowManager;
        private IWindow _sut;

        [TestInitialize]
        public void Setup()
        {
            _windowPtr = new IntPtr();
            _mockWindowManager = new Mock<IWindowManager>();
            _sut = new Window(_windowPtr, _mockWindowManager.Object);
        }

        [TestMethod]
        public void NameTest()
        {
            const string title ="Title";
            _mockWindowManager.Setup(mgr => mgr.GetTitle(It.IsAny<IntPtr>()))
                .Returns((IntPtr ptr) => title);

            string result = _sut.Name;

            _mockWindowManager.Verify(mgr => mgr.GetTitle(_windowPtr));
            result.ShouldBe(title);
        }

        [TestMethod]
        public void LocationTest()
        {
            _mockWindowManager.Setup(mgr => mgr.GetLocation(It.IsAny<IntPtr>()))
                              .Returns((IntPtr ptr) => new RECT() {Left = 10, Bottom = 40, Right = 20, Top = 30});

            WindowLocation result = _sut.Location;

            _mockWindowManager.Verify(mgr => mgr.GetLocation(_windowPtr));
            result.X.ShouldBe(10);
            result.Right.ShouldBe(20);
            result.Width.ShouldBe(10);
        }

        [TestMethod]
        public void ClassNameTest()
        {
            const string className = "Class Name";
            _mockWindowManager.Setup(mgr => mgr.GetClassName(It.IsAny<IntPtr>()))
                .Returns((IntPtr ptr) => className);

            string result = _sut.ClassName;

            _mockWindowManager.Verify(mgr => mgr.GetClassName(_windowPtr));
            result.ShouldBe(className);
        }

        [TestMethod]
        public void SetWindowPositionTest()
        {
            WindowLocation location = new WindowLocation(new RECT() { Left = 10, Bottom = 40, Right = 20, Top = 30 });

            _sut.SetLocation(location);

            _mockWindowManager.Verify(mgr => mgr.SetLocationUnchangedOrder(_windowPtr, It.IsAny<RECT>()));
        }

        [TestMethod]
        public void GetWindowState()
        {
            _mockWindowManager.Setup(mgr => mgr.GetShowState(It.IsAny<IntPtr>())).Returns(ShowState.SW_SHOW);

            ShowState state = _sut.State;

            state.ShouldBe(ShowState.SW_SHOW);
        }

        [TestMethod]
        public void IsTileableTest()
        {
            _mockWindowManager.Setup(mgr => mgr.IsWindowVisible(It.IsAny<IntPtr>())).Returns(true);
            _mockWindowManager.Setup(mgr => mgr.GetExtendedStyle(It.IsAny<IntPtr>()))
                              .Returns(ExtendedWindowStyleFlags.WS_EX_WINDOWEDGE);

            _sut.IsTileable().ShouldBe(true);
        }

        [TestMethod]
        public void IsNotTileableTest_NotVisible()
        {
            _mockWindowManager.Setup(mgr => mgr.IsWindowVisible(It.IsAny<IntPtr>())).Returns(false);
            _mockWindowManager.Setup(mgr => mgr.GetExtendedStyle(It.IsAny<IntPtr>()))
                              .Returns(ExtendedWindowStyleFlags.WS_EX_WINDOWEDGE);

            _sut.IsTileable().ShouldBe(false);
        }

        [TestMethod]
        public void IsNotTileableTest_NotWindowEdgeWindow()
        {
            _mockWindowManager.Setup(mgr => mgr.IsWindowVisible(It.IsAny<IntPtr>())).Returns(true);
            _mockWindowManager.Setup(mgr => mgr.GetExtendedStyle(It.IsAny<IntPtr>()))
                              .Returns(ExtendedWindowStyleFlags.WS_EX_CONTEXTHELP);

            _sut.IsTileable().ShouldBe(false);
        }

        [TestMethod]
        public void IsVisibleTest()
        {
            _mockWindowManager.Setup(mgr => mgr.IsWindowVisible(It.IsAny<IntPtr>())).Returns(true);

            bool result = _sut.IsVisible();

            result.ShouldBe(true);
        }
    }
}

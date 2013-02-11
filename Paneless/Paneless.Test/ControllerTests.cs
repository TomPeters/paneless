using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paneless.Core;
using System.Linq;
using EasyAssertions;

namespace Paneless.Test
{
    [TestClass]
    public class ControllerTests
    {
        private Mock<IDesktop> _mockDesktop;
        private Controller _sut;

        [TestInitialize]
        public void Setup()
        {
            _mockDesktop = new Mock<IDesktop>();
            _sut = new Controller();//_mockDesktop.Object);
        }

        [TestMethod]
        public void SetDefaultLayoutsTest()
        {
            _sut.SetDefaultLayouts();
            _sut.AssignWindows();
        }
    }
}

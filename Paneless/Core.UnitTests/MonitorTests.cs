using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Paneless.Core.UnitTests
{
    [TestClass]
    public class MonitorTests
    {
        private Monitor _sut;

        [TestInitialize]
        public void Setup()
        {
            _sut = new Monitor();
        }

        [TestMethod]
        public void AddWindowTest()
        {
            Mock<ITag> mockTag = new Mock<ITag>();
            IWindow mockWindow = new Mock<IWindow>().Object;
            _sut.Tag = mockTag.Object;

            _sut.AddWindow(mockWindow);

            mockTag.Verify(t => t.AddWindow(mockWindow));
        }
    }
}

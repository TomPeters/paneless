using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EasyAssertions;

namespace Paneless.Core.UnitTests
{
    [TestClass]
    public class TagTests
    {
        private Tag _sut;

        [TestInitialize]
        public void Setup()
        {
            _sut = new Tag();
        }

        [TestMethod]
        public void AddWindowTest()
        {
            int windowCount = _sut.Windows.Count;
            IWindow mockWindow = new Mock<IWindow>().Object;

            _sut.AddWindow(mockWindow);

            _sut.Windows.Count.ShouldBe(windowCount + 1);
        }
    }
}

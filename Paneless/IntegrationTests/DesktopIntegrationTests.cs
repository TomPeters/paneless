using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paneless.Core;
using EasyAssertions;

namespace Paneless.IntegrationTests
{
    [TestClass]
    public class DesktopIntegrationTests
    {
        [TestMethod]
        public void DesktoPopulateScreensTest()
        {
            Desktop sut = new Desktop();
            sut.PopulateScreens();
            sut.Screens.Count().ShouldBeGreaterThan(0); // Environment dependent so therefore an integration test
        }

        [TestMethod]
        public void DesktopPopulateWindowsTest()
        {
            Desktop sut = new Desktop();
            sut.PopulateWindows();
            sut.Windows.Count().ShouldBeGreaterThan(0);
        }
    }
}

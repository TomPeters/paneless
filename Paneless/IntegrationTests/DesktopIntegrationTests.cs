using System.Collections.Generic;
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
        public void DesktoPopulateMonitorsTest()
        {
            Desktop sut = new Desktop();
            sut.Monitors.Count().ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void DesktopGetWindows()
        {
            Desktop sut = new Desktop();
            List<IWindow> windows = sut.DetectWindows();
            windows.Count().ShouldBeGreaterThan(0);
        }
    }
}

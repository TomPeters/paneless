using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyAssertions;

namespace Paneless.IntegrationTests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void ControllerSetupTest()
        {
            Controller controller = new Controller();
            controller.Desktop.Monitors.Count.ShouldBeGreaterThan(0);
            controller.Desktop.Monitors[0].Tag.ShouldNotBeNull();
            controller.Desktop.Monitors[0].Tag.Windows.Count.ShouldBeGreaterThan(0);
            controller.Desktop.Monitors[1].Tag.Windows.Count.ShouldBeGreaterThan(0);
        }
    }
}

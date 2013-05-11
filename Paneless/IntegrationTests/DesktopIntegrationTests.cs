using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paneless.Core;
using EasyAssertions;
using Paneless.Core.Layouts;
using WinApi.Interface;
using WinApi.Windows7;

namespace Paneless.IntegrationTests
{
    [TestClass]
    public abstract class DesktopIntegrationTests
    {
        protected abstract IDesktopManager DesktopManager { get; }
        protected abstract IWindowManager WindowManager { get; }
        private readonly ILayoutFactory _layoutFactory = new EmptyLayoutFactory();

        [TestMethod]
        public void DesktoPopulateMonitorsTest()
        {
            Desktop sut = new Desktop(DesktopManager, WindowManager, _layoutFactory);
            sut.Monitors.Count().ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void DesktopGetWindows()
        {
            Desktop sut = new Desktop(DesktopManager, WindowManager, _layoutFactory);
            IEnumerable<IWindow> windows = sut.DetectWindows();
            windows.Count().ShouldBeGreaterThan(0);
        }
    }

    [TestClass]
    public class Windows7DesktopIntegrationTests : DesktopIntegrationTests
    {
        protected override IDesktopManager DesktopManager
        {
            get { return new DesktopManager(); }
        }

        protected override IWindowManager WindowManager
        {
            get { return new WindowManager(); }
        }
    }
}

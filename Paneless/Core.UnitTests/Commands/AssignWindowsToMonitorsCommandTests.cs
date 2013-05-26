using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paneless.Core.Commands;
using System.Linq;

namespace Paneless.Core.UnitTests.Commands
{
    [TestClass]
    public class AssignWindowsToMonitorsCommandTests
    {
        [TestMethod]
        public void AfterExecuteMonitorsAreAssignedWindowsFromDetectWindows()
        {
            Mock<IWindow> mockWindow1 = new Mock<IWindow>();
            Mock<IWindow> mockWindow2 = new Mock<IWindow>();

            Mock<IMonitor> mockMonitor1 = new Mock<IMonitor>();
            Mock<IMonitor> mockMonitor2 = new Mock<IMonitor>();

            IEnumerable<Mock<IMonitor>> mockMonitors = new[] {mockMonitor1, mockMonitor2};

            mockMonitor1.Setup(m => m.IsInSameScreen(mockWindow1.Object)).Returns(true);
            mockMonitor1.Setup(m => m.IsInSameScreen(mockWindow2.Object)).Returns(false);

            mockMonitor2.Setup(m => m.IsInSameScreen(mockWindow1.Object)).Returns(false);
            mockMonitor2.Setup(m => m.IsInSameScreen(mockWindow2.Object)).Returns(true);

            Mock<IDesktop> mockDesktop = new Mock<IDesktop>();
            mockDesktop.Setup(d => d.DetectWindows()).Returns(new[] { mockWindow1.Object, mockWindow2.Object });

            ICommand sut = new AssignWindowsToMonitorsCommand(mockDesktop.Object, mockMonitors.Select(mm => mm.Object));

            sut.Execute();

            foreach (Mock<IMonitor> mockMonitor in mockMonitors)
            {
                mockMonitor.Verify(m => m.ClearWindows(), Times.Once());
            }

            mockDesktop.Verify(d => d.DetectWindows(), Times.Once());

            mockMonitor1.Verify(m => m.AddWindow(mockWindow1.Object), Times.Once());
            mockMonitor1.Verify(m => m.AddWindow(mockWindow2.Object), Times.Never());

            mockMonitor2.Verify(m => m.AddWindow(mockWindow1.Object), Times.Never());
            mockMonitor2.Verify(m => m.AddWindow(mockWindow2.Object), Times.Once());
        }
    }
}

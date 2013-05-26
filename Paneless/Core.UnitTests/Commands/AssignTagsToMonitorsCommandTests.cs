using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paneless.Core.Commands;
using System.Linq;

namespace Paneless.Core.UnitTests.Commands
{
    [TestClass]
    public class AssignTagsToMonitorsCommandTests
    {
        [TestMethod]
        public void AfterExecuteNewTagsAssignedToMonitorsAndDesktop()
        {
            IList<Mock<IMonitor>> mockMonitors = new List<Mock<IMonitor>>();
            for (int i = 0; i < 5; i++)
            {
                mockMonitors.Add(new Mock<IMonitor>());
            }

            Mock<IDesktop> mockDesktop = new Mock<IDesktop>();

            ICommand sut = new AssignTagsToMonitorsCommand(mockMonitors.Select(m => m.Object), mockDesktop.Object);

            sut.Execute();

            foreach (Mock<IMonitor> monitor in mockMonitors)
            {
                monitor.VerifySet(m => m.Tag, Times.Once());
            }

            mockDesktop.Verify(d => d.AddTag(It.IsAny<ITag>()), Times.Exactly(mockMonitors.Count));
        }
    }
}

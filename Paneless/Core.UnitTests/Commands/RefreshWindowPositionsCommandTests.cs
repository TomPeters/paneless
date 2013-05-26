using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paneless.Core.Commands;
using System.Linq;

namespace Paneless.Core.UnitTests.Commands
{
    [TestClass]
    public class RefreshWindowPositionsCommandTests
    {
        [TestMethod]
        public void AfterExecuteAllActiveTagsHaveTileMethodCalled()
        {
            IList<Mock<ITag>> mockTags = new List<Mock<ITag>>();
            for (int i = 0; i < 5; i++)
            {
                mockTags.Add(new Mock<ITag>());
            }

            ICommand sut = new RefreshWindowPositionsCommand(mockTags.Select(mt => mt.Object));

            sut.Execute();

            foreach (Mock<ITag> mockTag in mockTags)
            {
                mockTag.Verify(t => t.Tile(), Times.Once());
            }
        }
    }
}

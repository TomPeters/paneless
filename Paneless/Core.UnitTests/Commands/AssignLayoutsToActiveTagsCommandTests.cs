using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paneless.Core.Commands;
using Paneless.Core.Layouts;
using System.Linq;

namespace Paneless.Core.UnitTests.Commands
{
    [TestClass]
    public class AssignLayoutsToActiveTagsCommandTests
    {
        [TestMethod]
        public void AfterExecuteAllTagsAreSetToTheProvidedLayout()
        {
            IList<Mock<ITag>> mockActiveTags = new List<Mock<ITag>>();
            for (int i = 0; i < 5; i++)
            {
                mockActiveTags.Add(new Mock<ITag>());
            }
            ILayout layout = Mock.Of<ILayout>();
            ICommand sut = new AssignLayoutsToActiveTagsCommand(mockActiveTags.Select(at => at.Object), layout);

            sut.Execute();

            foreach (Mock<ITag> tagMock in mockActiveTags)
            {
                tagMock.Verify(t => t.SetLayout(layout), Times.Once());
            }
        }
    }
}

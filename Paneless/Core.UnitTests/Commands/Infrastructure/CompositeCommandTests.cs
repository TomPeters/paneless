using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paneless.Core.Commands;
using Paneless.Core.Events;

namespace Paneless.Core.UnitTests.Commands.Infrastructure
{
    [TestClass]
    public class CompositeCommandTests
    {
        private Mock<ICommand> _mockCommand1;
        private Mock<ICommand> _mockCommand2;
        private ICommand _sut;

        [TestInitialize]
        public void Setup()
        {
            _mockCommand1 = new Mock<ICommand>();
            _mockCommand2 = new Mock<ICommand>();
            _sut = new CompositeCommand(new List<ICommand>
                {
                    _mockCommand1.Object,
                    _mockCommand2.Object
                });
        }

        [TestMethod]
        public void ChildCommandAreExecutedWhenCompositeCommandIsExecuted()
        {
            _sut.Execute();

            _mockCommand1.Verify(c => c.Execute(), Times.Once());
            _mockCommand2.Verify(c => c.Execute(), Times.Once());
        }
    }
}

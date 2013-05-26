using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paneless.Core.Commands;
using Paneless.Core.Events;

namespace Paneless.Core.UnitTests.Commands.Infrastructure
{
    [TestClass]
    public class CompositeCommandFactoryTests
    {
        [TestMethod]
        public void CreateCommandReturnsCommandsFromCommandFactoriesAndCreatesCompositeCommandFromThem()
        {
            Mock<ICommand> command1 = new Mock<ICommand>();
            Mock<ICommand> command2 = new Mock<ICommand>();

            Mock<ICommandFactory> commandFactory1 = new Mock<ICommandFactory>();
            Mock<ICommandFactory> commandFactory2 = new Mock<ICommandFactory>();

            IEventArguments eventArguments = Mock.Of<IEventArguments>();

            commandFactory1.Setup(cf => cf.CreateCommand(It.IsAny<IEventArguments>())).Returns(command1.Object);
            commandFactory2.Setup(cf => cf.CreateCommand(It.IsAny<IEventArguments>())).Returns(command2.Object);

            ICommandFactory sut = new CompositeCommandFactory(new List<ICommandFactory>()
                {
                    commandFactory1.Object,
                    commandFactory2.Object
                });

            ICommand result = sut.CreateCommand(eventArguments);

            result.Execute();

            command1.Verify(c => c.Execute(), Times.Once());
            command2.Verify(c => c.Execute(), Times.Once());

            commandFactory1.Verify(cf => cf.CreateCommand(eventArguments), Times.Once());
            commandFactory2.Verify(cf => cf.CreateCommand(eventArguments), Times.Once());
        }
    }
}

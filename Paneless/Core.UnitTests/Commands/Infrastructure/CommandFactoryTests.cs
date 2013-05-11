using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paneless.Core.Commands;

namespace Paneless.Core.UnitTests.Commands.Infrastructure
{
    [TestClass]
    public class CommandFactoryTests
    {
        [TestMethod]
        public void CreateCommandReturnsCommandsFromCommandFactoriesAndCreatesCompositeCommandFromThem()
        {
            Mock<ICommand> command1 = new Mock<ICommand>();
            Mock<ICommand> command2 = new Mock<ICommand>();

            Mock<ICommandFactory> commandFactory1 = new Mock<ICommandFactory>();
            Mock<ICommandFactory> commandFactory2 = new Mock<ICommandFactory>();

            commandFactory1.Setup(cf => cf.CreateCommand()).Returns(command1.Object);
            commandFactory2.Setup(cf => cf.CreateCommand()).Returns(command2.Object);

            ICommandFactory sut = new CommandFactory(new List<ICommandFactory>()
                {
                    commandFactory1.Object,
                    commandFactory2.Object
                });

            ICommand result = sut.CreateCommand();

            result.Execute();

            command1.Verify(c => c.Execute(), Times.Once());
            command2.Verify(c => c.Execute(), Times.Once());
        }
    }
}

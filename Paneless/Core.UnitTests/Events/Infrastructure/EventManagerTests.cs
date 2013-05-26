using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paneless.Core.Commands;
using Paneless.Core.Events;

namespace Paneless.Core.UnitTests.Events.Infrastructure
{
    [TestClass]
    public class EventManagerTests
    {
        private Mock<ICommandEventFactory> _mockCommandEventFactory;
        private Mock<ICommand> _mockCommand;
        private EventManager _sut;
        private Mock<ITriggeredEvent> _mockTriggeredEvent;

        [TestInitialize]
        public void Setup()
        {
            _mockCommandEventFactory = new Mock<ICommandEventFactory>();
            _mockCommand = new Mock<ICommand>();
            _mockTriggeredEvent = new Mock<ITriggeredEvent>();
            _mockCommandEventFactory.Setup(cef => cef.CreateCommandFromEvent(It.IsAny<ITriggeredEvent>())).Returns(_mockCommand.Object);
            _sut = new EventManager(_mockCommandEventFactory.Object);
        }

        [TestMethod]
        public void TriggerEventCallsCommandEventFactoryMethodWithEvent()
        {
            _sut.TriggerEvent(_mockTriggeredEvent.Object);

            _mockCommandEventFactory.Verify(cef => cef.CreateCommandFromEvent(_mockTriggeredEvent.Object), Times.Once());
        }

        [TestMethod]
        public void TriggerEventExecutesTheReturnedCommand()
        {
            _sut.TriggerEvent(_mockTriggeredEvent.Object);

            _mockCommand.Verify(c => c.Execute(), Times.Once());
        }
    }
}

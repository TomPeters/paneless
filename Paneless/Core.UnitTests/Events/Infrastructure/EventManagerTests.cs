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
            _mockCommandEventFactory.Setup(cef => cef.CreateCommandFromEvent(It.IsAny<IEvent>())).Returns(_mockCommand.Object);
            _sut = new EventManager(_mockCommandEventFactory.Object);
        }

        [TestMethod]
        public void TriggerEventCallsCommandEventFactoryMethodWithEvent()
        {
            
            Mock<IEvent> mockEvent = new Mock<IEvent>();
            _mockTriggeredEvent.Setup(te => te.Event).Returns(mockEvent.Object);

            _sut.TriggerEvent(_mockTriggeredEvent.Object);

            _mockCommandEventFactory.Verify(cef => cef.CreateCommandFromEvent(mockEvent.Object), Times.Once());
        }

        [TestMethod]
        public void TriggerEventExecutesTheReturnedCommand()
        {
            _sut.TriggerEvent(_mockTriggeredEvent.Object);

            _mockCommand.Verify(c => c.Execute(), Times.Once());
        }

        [TestMethod]
        public void TriggerEventAssignsEventArgumentsToCommand()
        {
            Mock<IEventArguments> mockEventArguments = new Mock<IEventArguments>();
            _mockTriggeredEvent.Setup(te => te.EventArguments).Returns(mockEventArguments.Object);

            _sut.TriggerEvent(_mockTriggeredEvent.Object);

            _mockCommand.VerifySet(c => c.EventArguments = mockEventArguments.Object);
        }
    }
}

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paneless.Core.Events;
using EasyAssertions;

namespace Paneless.Core.UnitTests.Events.Infrastructure
{
    [TestClass]
    public class EventBuilderTests
    {
        private EventBuilder _sut;

        [TestInitialize]
        public void Setup()
        {
            _sut = new EventBuilder();
        }

        [TestMethod]
        public void NoEventsReturnsEmptyEvent()
        {
            ITriggeredEvent result = _sut.Build();
            result.Event.ShouldBeA<EmptyEvent>();
        }

        [TestMethod]
        public void AddedEventsShouldReturnCompositeEventWithAddedEvents()
        {
            _sut.AddEvent(new StartupEvent());
            _sut.AddEvent(new WindowMovedEvent());
            ITriggeredEvent result = _sut.Build();

            result.Event.ShouldBeA<CompositeEvent>();
            result.Event.Equals(new CompositeEvent(new List<IEvent>() {
                new StartupEvent(),
                new WindowMovedEvent()
            })).ShouldBe(true);
        }

        [TestMethod]
        public void SingleAddedEventShouldReturnCompositeEventWithAddedEvent()
        {
            _sut.AddEvent(new StartupEvent());
            ITriggeredEvent result = _sut.Build();

            result.Event.ShouldBeA<CompositeEvent>();
            result.Event.Equals(new StartupEvent()).ShouldBe(true);
        }
    }
}

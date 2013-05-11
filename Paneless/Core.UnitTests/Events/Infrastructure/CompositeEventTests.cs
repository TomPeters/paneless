using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paneless.Core.Events;
using EasyAssertions;

namespace Paneless.Core.UnitTests.Events.Infrastructure
{
    [TestClass]
    public class CompositeEventTests
    {
        [TestMethod]
        public void TwoEmptyCompositeEventsAreNotEqual()
        {
            IEvent compositeEvent1 = new CompositeEvent(new List<IEvent>());
            IEvent compositeEvent2 = new CompositeEvent(new List<IEvent>());
            CheckEventEquality(compositeEvent1, compositeEvent2, false);
        }



        [TestMethod]
        public void CompositeEventsOfDifferentSizeAreNotEqual()
        {
            IEvent compositeEvent1 = new CompositeEvent(new List<IEvent>()
                {
                    new StartupEvent()
                });
            IEvent compositeEvent2 = new CompositeEvent(new List<IEvent>());
            CheckEventEquality(compositeEvent1, compositeEvent2, false);
        }

        [TestMethod]
        public void SingleCompositeEventsAreNotEqualIfChildrenDiffer()
        {
            IEvent compositeEvent1 = new CompositeEvent(new List<IEvent>()
                {
                    new StartupEvent()
                });
            IEvent compositeEvent2 = new CompositeEvent(new List<IEvent>()
                {
                    new ShutdownEvent()
                });
            CheckEventEquality(compositeEvent1, compositeEvent2, false);
        }

        [TestMethod]
        public void SingleCompositeEventsAreEqualIfChildrenAreEqual()
        {
            IEvent compositeEvent1 = new CompositeEvent(new List<IEvent>()
                {
                    new StartupEvent()
                });
            IEvent compositeEvent2 = new CompositeEvent(new List<IEvent>()
                {
                    new StartupEvent()
                });
            CheckEventEquality(compositeEvent1, compositeEvent2, true);
        }

        [TestMethod]
        public void SingleCompositeEventsIsEqualToSingleEventOfSameType()
        {
            IEvent compositeEvent1 = new CompositeEvent(new List<IEvent>()
                {
                    new StartupEvent()
                });
            IEvent singleEvent = new StartupEvent();
            CheckEventEquality(compositeEvent1, singleEvent, true);
        }

        [TestMethod]
        public void MultipleCompositeEventsAreEqualIfAllChildrenMatch()
        {
            IEvent compositeEvent1 = new CompositeEvent(new List<IEvent>()
                {
                    new StartupEvent(),
                    new WindowMovedEvent()
                });
            IEvent compositeEvent2 = new CompositeEvent(new List<IEvent>()
                {
                    new StartupEvent(),
                    new WindowMovedEvent()
                });
            CheckEventEquality(compositeEvent1, compositeEvent2, true);
        }

        [TestMethod]
        public void MultipleCompositeEventsEqualIfChildrenMatchAndHaveDifferentOrders()
        {
            IEvent compositeEvent1 = new CompositeEvent(new List<IEvent>()
                {
                    new StartupEvent(),
                    new WindowMovedEvent()
                });
            IEvent compositeEvent2 = new CompositeEvent(new List<IEvent>()
                {
                    new WindowMovedEvent(),
                    new StartupEvent()
                });
            CheckEventEquality(compositeEvent1, compositeEvent2, true);
        }

        [TestMethod]
        public void MultipleCompositeEventsNotEqualIfEventsDontMatch()
        {
            IEvent compositeEvent1 = new CompositeEvent(new List<IEvent>()
                {
                    new StartupEvent(),
                    new WindowMovedEvent()
                });
            IEvent compositeEvent2 = new CompositeEvent(new List<IEvent>()
                {
                    new ShutdownEvent(),
                    new WindowMovedEvent()
                });
            CheckEventEquality(compositeEvent1, compositeEvent2, false);
        }

        [TestMethod]
        public void MultipleCompositeEventsNotEqualIfAllEventsDontMatch()
        {
            IEvent compositeEvent1 = new CompositeEvent(new List<IEvent>()
                {
                    new StartupEvent(),
                    new WindowMovedEvent()
                });
            IEvent compositeEvent2 = new CompositeEvent(new List<IEvent>()
                {
                    new StartupEvent()
                });
            CheckEventEquality(compositeEvent1, compositeEvent2, false);
        }

        private static void CheckEventEquality(IEvent compositeEvent1, IEvent compositeEvent2, bool expected)
        {
            compositeEvent1.Equals(compositeEvent2).ShouldBe(expected);
            compositeEvent2.Equals(compositeEvent1).ShouldBe(expected);
        }
    }
}

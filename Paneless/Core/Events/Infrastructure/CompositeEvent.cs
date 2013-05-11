using System.Collections.Generic;
using System.Linq;

namespace Paneless.Core.Events
{
    public class CompositeEvent : IEvent
    {
        private IEnumerable<IEvent> Events { get; set; } 

        public CompositeEvent(IEnumerable<IEvent> events)
        {
            Events = events;
        }

        public bool Equals(IEvent other)
        {
            if ((other is CompositeEvent)) return Equals(other as CompositeEvent);
            return Events.Count() == 1 && Events.Single().Equals(other);
        }

        private bool Equals(CompositeEvent other)
        {
            return Events.Any() 
                && Events.Count() == other.Events.Count() 
                && Events.All(curEvent => EventInOtherCompositeEvent(curEvent, other));
        }

        private bool EventInOtherCompositeEvent(IEvent curEvent, CompositeEvent other)
        {
            return other.Events.Any(otherEvent => otherEvent.Equals(curEvent));
        }
    }
}

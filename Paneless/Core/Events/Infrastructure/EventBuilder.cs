using System.Collections.Generic;

namespace Paneless.Core.Events
{
    public class EventBuilder : IEventBuilder
    {
        public EventBuilder()
            :this(new EventArguments())
        {
        }

        private EventBuilder(EventArguments eventArguments)
        {
            Arguments = eventArguments;
            EventList = new List<IEvent>();
        }

        public IEventArguments Arguments { get; private set; }
        private IList<IEvent> EventList { get; set; }  

        public void AddEvent(IEvent ev)
        {
            EventList.Add(ev);
        }

        public ITriggeredEvent Build()
        {
            IEvent compositeEvent;
            if (EventList.Count == 0)
                compositeEvent = new EmptyEvent();
            else
                compositeEvent = new CompositeEvent(EventList);
            return new TriggeredEvent(compositeEvent, Arguments);
        }
    }

    public interface IEventBuilder
    {
        IEventArguments Arguments { get; }
        void AddEvent(IEvent ev);
        ITriggeredEvent Build();
    }
}

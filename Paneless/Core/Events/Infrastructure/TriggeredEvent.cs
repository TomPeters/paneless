namespace Paneless.Core.Events
{
    public class TriggeredEvent : ITriggeredEvent
    {
        public IEvent Event { get; private set; }
        public IEventArguments EventArguments { get; private set; }

        public TriggeredEvent(IEvent ev, IEventArguments eventArguments)
        {
            Event = ev;
            EventArguments = eventArguments;
        }
    }

    public interface ITriggeredEvent
    {
        IEvent Event { get; }
        IEventArguments EventArguments { get; }
    }
}

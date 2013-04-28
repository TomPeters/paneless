namespace Paneless.Core.Events
{
    public class LoggedEvent : IEvent
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ILoggableEvent _wrappedEvent;

        internal LoggedEvent(ILoggableEvent wrappedEvent)
        {
            _wrappedEvent = wrappedEvent;
        }

        public void FireEvent()
        {
            Logger.Debug("Firing event: " + _wrappedEvent.LogDescription);
            _wrappedEvent.FireEvent();
        }
    }
}

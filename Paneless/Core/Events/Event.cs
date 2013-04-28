namespace Paneless.Core.Events
{
    public abstract class Event : ILoggableEvent
    {
        protected IController Controller { get; private set; }
        protected Event(IController controller)
        {
            Controller = controller;
        }

        public abstract void FireEvent();
        public abstract string LogDescription { get; }
    }
    public interface IEvent
    {
        void FireEvent();
    }
}

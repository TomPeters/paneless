namespace Paneless.Core.Events
{
    public abstract class Event : IEvent
    {
        protected IController Controller { get; private set; }
        protected Event(IController controller)
        {
            Controller = controller;
        }

        public abstract void FireEvent();
    }
    public interface IEvent
    {
        void FireEvent();
    }
}

namespace Paneless.Core.Events
{
    public abstract class Event : IEvent
    {
        protected IController Controller { get; private set; }
        protected Event(IController controller)
        {
            Controller = controller;
        }
        public void FireEvent()
        {
            PerformAction();
        }

        public abstract void PerformAction();
    }
    public interface IEvent
    {
        void FireEvent();
        void PerformAction();
    }
}

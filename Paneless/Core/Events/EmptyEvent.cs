namespace Paneless.Core.Events
{
    public class EmptyEvent : Event
    {
        public EmptyEvent(IController controller) 
            : base(controller)
        {
        }

        public override void FireEvent()
        {
        }
    }
}

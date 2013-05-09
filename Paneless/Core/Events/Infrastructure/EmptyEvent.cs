namespace Paneless.Core.Events
{
    public class EmptyEvent : Event<EmptyEvent>
    {
        protected override bool Equals(EmptyEvent other)
        {
            return false;
        }
    }
}

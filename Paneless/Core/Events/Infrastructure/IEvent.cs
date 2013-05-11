using System;

namespace Paneless.Core.Events
{
    public abstract class Event<T> : IEvent where T: class, IEvent
    {
        public bool Equals(IEvent other)
        {
            if (other is CompositeEvent) return other.Equals(this);
            if (!(other is T)) return false;
            return Equals(other as T);
        }

        protected virtual bool Equals(T other)
        {
            return true;
        }
    }

    public interface IEvent : IEquatable<IEvent>
    {
    }
}

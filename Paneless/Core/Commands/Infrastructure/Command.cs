﻿using Paneless.Core.Events;

namespace Paneless.Core.Commands
{
    public abstract class Command : ICommand
    {
        public IEventArguments EventArguments { protected get; set; }
        public abstract void Execute();
        protected IDomainObjectProvider DomainObjectProvider { get { return EventArguments.DomainObjectProvider; }} // For convenience
    }

    public interface ICommand
    {
        IEventArguments EventArguments { set; }
        void Execute();
    }
}

﻿using Paneless.Core.Events;

namespace Paneless.Core.Commands
{
    class ClearLayoutsOnActiveTagsCommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(IEventArguments eventArguments)
        {
            IDomainObjectProvider domainObjectProvider = eventArguments.DomainObjectProvider;

            return new AssignLayoutsToActiveTagsCommand(domainObjectProvider.ActiveTags, domainObjectProvider.Desktop.LayoutFactory.CreateLayout("EmptyLayout"));
        }
    }
}
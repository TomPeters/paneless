using Paneless.Core.Events;

namespace Paneless.Core.Commands
{
    public class AssignTagsToMonitorsCommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(IEventArguments eventArguments)
        {
            IDomainObjectProvider domainObjectProvider = eventArguments.DomainObjectProvider;

            return new AssignTagsToMonitorsCommand(domainObjectProvider.Monitors, domainObjectProvider.Desktop);
        }
    }
}

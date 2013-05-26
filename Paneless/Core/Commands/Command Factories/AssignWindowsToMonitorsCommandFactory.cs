using Paneless.Core.Events;

namespace Paneless.Core.Commands
{
    public class AssignWindowsToMonitorsCommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(IEventArguments eventArguments)
        {
            IDomainObjectProvider domainObjectProvider = eventArguments.DomainObjectProvider;

            return new AssignWindowsToMonitorsCommand(domainObjectProvider.Desktop, domainObjectProvider.Monitors);
        }
    }
}

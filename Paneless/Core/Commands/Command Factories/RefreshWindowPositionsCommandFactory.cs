using Paneless.Core.Events;

namespace Paneless.Core.Commands
{
    public class RefreshWindowPositionsCommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(IEventArguments eventArguments)
        {
            return new RefreshWindowPositionsCommand(eventArguments.DomainObjectProvider.ActiveTags);
        }
    }
}

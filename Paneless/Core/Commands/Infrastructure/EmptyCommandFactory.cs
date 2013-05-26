using Paneless.Core.Events;

namespace Paneless.Core.Commands
{
    public class EmptyCommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(IEventArguments eventArguments)
        {
            return new EmptyCommand();
        }
    }
}

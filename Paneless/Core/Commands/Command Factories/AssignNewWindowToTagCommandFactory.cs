using Paneless.Core.Events;

namespace Paneless.Core.Commands
{
    public class AssignNewWindowToTagCommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(IEventArguments eventArguments)
        {
            return new AssignNewWindowToTagCommand(eventArguments.DomainObjectProvider.WindowFactory.CreateWindow(eventArguments.Hwnd), 
                                                    eventArguments.DomainObjectProvider.Monitors, eventArguments.DomainObjectProvider.Desktop);
        }
    }
}

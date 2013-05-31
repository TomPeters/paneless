using Paneless.Core.Events;

namespace Paneless.Core.Commands.Command_Factories
{
   public class RemoveWindowCommandFactory : ICommandFactory
    {
       public ICommand CreateCommand(IEventArguments eventArguments)
       {
           return new RemoveWindowCommand(eventArguments.DomainObjectProvider.WindowFactory.CreateWindow(eventArguments.Hwnd),
                   eventArguments.DomainObjectProvider.Desktop, eventArguments.DomainObjectProvider.ActiveTags);
       }
    }
}

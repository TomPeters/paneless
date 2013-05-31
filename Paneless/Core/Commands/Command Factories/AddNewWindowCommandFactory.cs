using Paneless.Core.Events;

namespace Paneless.Core.Commands.Command_Factories
{
    public class AddNewWindowCommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(IEventArguments eventArguments)
        {
            IWindow window = eventArguments.DomainObjectProvider.WindowFactory.CreateWindow(eventArguments.Hwnd);

            if (NewWindowShouldBeAdded(window, eventArguments.DomainObjectProvider.Desktop))
                return new CompositeCommand(new [] {
                    new AssignNewWindowToTagCommandFactory().CreateCommand(eventArguments), 
                    new RefreshWindowPositionsCommandFactory().CreateCommand(eventArguments)
                });
            return new EmptyCommand();
        }

        private static bool NewWindowShouldBeAdded(IWindow window, IDesktop desktop)
        {
            
            return window.IsTileable() && !desktop.IsManagingWindow(window);
        }
    }
}

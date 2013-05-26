using Paneless.Core.Commands;

namespace Paneless.Core.Events
{
    public class EventManager : IEventManager
    {
        private ICommandEventFactory CommandEventFactory { get; set; }

        public EventManager(ICommandEventFactory commandEventFactory)
        {
            CommandEventFactory = commandEventFactory;
        }

        public void TriggerEvent(ITriggeredEvent ev)
        {
            ICommand command = CommandEventFactory.CreateCommandFromEvent(ev);
            command.Execute();
        }
    }

    public interface IEventManager
    {
        void TriggerEvent(ITriggeredEvent ev);
    }
}

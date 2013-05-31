using System.Collections.Generic;
using Paneless.Core.Commands.Command_Factories;
using Paneless.Core.Events;

namespace Paneless.Core.Commands
{
    // TODO: Need to break up logic with setting up command mappings to make this class testable
    // This will probably happen when configuration/settings is added
    public class CommandEventFactory : ICommandEventFactory
    {
        private readonly IList<KeyValuePair<IEvent, ICommandFactory>> _commandMapping; 

        public CommandEventFactory()
        {
            _commandMapping = new List<KeyValuePair<IEvent, ICommandFactory>>();
            SetupCommandMapping();
        }

        // TODO: This method will ultimately be replaced by something that reads from settings/configuration. Maybe a command mapper class
        private void SetupCommandMapping()
        {
            AddCommandMapping(new StartupEvent(), new CompositeCommandFactory(new List<ICommandFactory>
                {
                    new AssignTagsToMonitorsCommandFactory(),
                    new AssignWindowsToMonitorsCommandFactory(),
                    new ClearLayoutsOnActiveTagsCommandFactory(),
                }));
            AddCommandMapping(new KeyEvent(HotkeyEvents.Tile), new CompositeCommandFactory(new List<ICommandFactory>
                {
                    new AssignLayoutsToActiveTagsCommandFactory(),
                    new AssignWindowsToMonitorsCommandFactory(),
                    new RefreshWindowPositionsCommandFactory()
                }));
            AddCommandMapping(new KeyEvent(HotkeyEvents.Untile), new ClearLayoutsOnActiveTagsCommandFactory());
            AddCommandMapping(new WindowMovedEvent(), new RefreshWindowPositionsCommandFactory());
            AddCommandMapping(new WindowResizedEvent(), new RefreshWindowPositionsCommandFactory());
            AddCommandMapping(new WindowResizingEvent(), new RefreshWindowPositionsCommandFactory());
            AddCommandMapping(new WindowShownEvent(), new AddNewWindowCommandFactory());
            AddCommandMapping(new WindowDestructionEvent(), new CompositeCommandFactory(new List<ICommandFactory>
                {
                    new RemoveWindowCommandFactory(),
                    new RefreshWindowPositionsCommandFactory()
                }));
        }

        private void AddCommandMapping(IEvent ev, ICommandFactory commandFactory)
        {
            _commandMapping.Add(new KeyValuePair<IEvent, ICommandFactory>(ev, commandFactory));
        }

        public ICommand CreateCommandFromEvent(ITriggeredEvent ev)
        {
            ICommandFactory commandFactory = GetCommandFactory(ev.Event);
            ICommand command = commandFactory.CreateCommand(ev.EventArguments);
            return WrapCommandForLogging(command);
        }

        private ICommand WrapCommandForLogging(ICommand command)
        {
            return new LoggedCommandProxy(command);
        }

        private ICommandFactory GetCommandFactory(IEvent ev)
        {
            IList<ICommandFactory> commandFactories = GetCommandFactoriesFromEvent(ev);
            if (commandFactories.Count == 0)
                return new EmptyCommandFactory();
            return new CompositeCommandFactory(commandFactories);
        }

        private IList<ICommandFactory> GetCommandFactoriesFromEvent(IEvent ev)
        {
            IList<ICommandFactory> commandFactories = new List<ICommandFactory>();
            foreach (KeyValuePair<IEvent, ICommandFactory> mappedCommand in _commandMapping)
            {
                if (ev.Equals(mappedCommand.Key))
                {
                    commandFactories.Add(mappedCommand.Value);
                }
            }
            return commandFactories;
        }
    }

    public interface ICommandEventFactory // TODO: Try to think of a better name for this
    {
        ICommand CreateCommandFromEvent(ITriggeredEvent ev);
    }
}

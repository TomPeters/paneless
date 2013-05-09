using System.Collections.Generic;
using System.ComponentModel;
using Paneless.Core.Events;
using System.Linq;

namespace Paneless.Core.Commands
{
    public class CommandEventFactory : ICommandEventFactory
    {
        private readonly IList<KeyValuePair<IEvent, ICommandFactory>> _commandMapping; 
        private ICommandFactory EmptyCommandFactory { get; set; }

        public CommandEventFactory()
        {
            _commandMapping = new List<KeyValuePair<IEvent, ICommandFactory>>();
            EmptyCommandFactory = new SingleCommandFactory<EmptyCommand>();
            SetupCommandMapping();
        }

        // This method will ultimately be replaced by something that reads from settings/configuration. Maybe a command mapper class
        private void SetupCommandMapping()
        {
            AddCommandMapping(new StartupEvent(), new CommandFactory(new List<ICommandFactory>
                {
                    new SingleCommandFactory<AssignTagsToMonitorsCommand>(),
                    new SingleCommandFactory<AssignWindowsToMonitorsCommand>(),
                    new SingleCommandFactory<ClearLayoutsOnActiveTagsCommand>(),
                }));
            AddCommandMapping(new KeyEvent(HotkeyEvents.Tile), new CommandFactory(new List<ICommandFactory>
                {
                    new SingleCommandFactory<AssignLayoutsToActiveTagsCommand>(),
                    new SingleCommandFactory<AssignWindowsToMonitorsCommand>(),
                    new SingleCommandFactory<RefreshWindowPositionsCommand>()
                }));
            AddCommandMapping(new KeyEvent(HotkeyEvents.Untile), new SingleCommandFactory<ClearLayoutsOnActiveTagsCommand>());
            AddCommandMapping(new WindowMovedEvent(), new SingleCommandFactory<RefreshWindowPositionsCommand>());
            AddCommandMapping(new WindowResizedEvent(), new SingleCommandFactory<RefreshWindowPositionsCommand>());
            AddCommandMapping(new WindowResizingEvent(), new SingleCommandFactory<RefreshWindowPositionsCommand>());
        }

        private void AddCommandMapping(IEvent ev, ICommandFactory commandFactory)
        {
            _commandMapping.Add(new KeyValuePair<IEvent, ICommandFactory>(ev, commandFactory));
        }

        public ICommand CreateCommandFromEvent(IEvent ev)
        {
            ICommandFactory commandFactory = GetCommandFactory(ev);
            ICommand command = commandFactory.CreateCommand();
            return WrapCommandForLogging(command);
        }

        private ICommand WrapCommandForLogging(ICommand command)
        {
            return new LoggedCommandProxy(command);
        }

        private ICommandFactory GetCommandFactory(IEvent ev)
        {
            IList<ICommandFactory> commandFactories = new List<ICommandFactory>();
            foreach (KeyValuePair<IEvent, ICommandFactory> mappedCommand in _commandMapping)
            {
                if (ev.Equals(mappedCommand.Key))
                {
                    commandFactories.Add(mappedCommand.Value);
                }
            }
            //_commandMapping.Where(cm => ev.Equals(cm.Key)).Select(mappedCommand => mappedCommand.Value).ToList());
            if (commandFactories.Count == 0)
                return new SingleCommandFactory<EmptyCommand>();
            return new CommandFactory(commandFactories);
        }
    }

    public interface ICommandEventFactory // Think of a better name for this
    {
        ICommand CreateCommandFromEvent(IEvent ev);
    }
}

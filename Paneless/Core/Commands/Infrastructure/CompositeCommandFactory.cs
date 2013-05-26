using System.Collections.Generic;
using System.Linq;
using Paneless.Core.Events;

namespace Paneless.Core.Commands
{
    public class CompositeCommandFactory : ICommandFactory
    {
        public CompositeCommandFactory(IEnumerable<ICommandFactory> childCommandFactories)
        {
            ChildCommandFactories = childCommandFactories;
        }

        private IEnumerable<ICommandFactory> ChildCommandFactories { get; set; }

        public ICommand CreateCommand(IEventArguments eventArguments)
        {
            IEnumerable<ICommand> childCommands = ChildCommandFactories.Select(cf => cf.CreateCommand(eventArguments)).ToList();
            return new CompositeCommand(childCommands);
        }
    }

    public interface ICommandFactory
    {
        ICommand CreateCommand(IEventArguments eventArguments);
    }
}

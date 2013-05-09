using System.Collections.Generic;
using System.Linq;

namespace Paneless.Core.Commands
{
    public class CommandFactory : ICommandFactory
    {
        public CommandFactory(IEnumerable<ICommandFactory> childCommandFactories)
        {
            ChildCommandFactories = childCommandFactories;
        }

        private IEnumerable<ICommandFactory> ChildCommandFactories { get; set; }

        public ICommand CreateCommand()
        {
            IEnumerable<ICommand> childCommands = ChildCommandFactories.Select(cf => cf.CreateCommand()).ToList();
            return new CompositeCommand(childCommands);
        }
    }

    public interface ICommandFactory
    {
        ICommand CreateCommand();
    }
}

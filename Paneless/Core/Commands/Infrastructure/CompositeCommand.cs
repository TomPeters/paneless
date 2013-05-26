using System.Collections.Generic;
using Paneless.Common;
using System.Linq;

namespace Paneless.Core.Commands
{
    public class CompositeCommand : ICommand, ILoggable
    {
        public CompositeCommand(IEnumerable<ICommand> childCommands)
        {
            ChildCommands = childCommands;
        }

        private IEnumerable<ICommand> ChildCommands { get; set; }

        public void Execute()
        {
            foreach (ICommand childCommand in ChildCommands)
            {
                childCommand.Execute();
            }
        }

        public string LogDescription
        {
            get
            {
                string output = "Composite Command: ";
                foreach (ILoggable childCommand in ChildCommands.Select(cc => cc as ILoggable).Where(l => l != null))
                {
                    output += childCommand.LogDescription + "; ";
                }
                return output;
            }
        }
    }
}

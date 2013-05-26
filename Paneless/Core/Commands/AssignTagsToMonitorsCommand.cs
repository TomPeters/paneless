using System.Collections.Generic;
using Paneless.Common;

namespace Paneless.Core.Commands
{
    public class AssignTagsToMonitorsCommand : ICommand, ILoggable
    {
        public AssignTagsToMonitorsCommand(IEnumerable<IMonitor> monitors, IDesktop desktop)
        {
            Monitors = monitors;
            Desktop = desktop;
        }

        private IEnumerable<IMonitor> Monitors { get; set; }
        private IDesktop Desktop { get; set; }

        public void Execute()
        {
            foreach (IMonitor monitor in Monitors)
            {
                ITag newTag = new Tag();
                Desktop.AddTag(newTag);
                monitor.Tag = newTag;
            }
        }

        public string LogDescription
        {
            get { return "Assigning tags to monitors"; }
        }
    }
}

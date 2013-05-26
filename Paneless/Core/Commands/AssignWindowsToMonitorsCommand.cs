using System.Collections.Generic;
using System.Linq;
using Paneless.Common;

namespace Paneless.Core.Commands
{
    public class AssignWindowsToMonitorsCommand : ICommand, ILoggable
    {
        public AssignWindowsToMonitorsCommand(IDesktop desktop, IEnumerable<IMonitor> monitors)
        {
            Desktop = desktop;
            Monitors = monitors;
        }

        private IEnumerable<IMonitor> Monitors { get; set; }

        private IDesktop Desktop { get; set; }

        public void Execute()
        {
            ClearWindows();
            AssignWindows();
        }

        private void ClearWindows()
        {
            foreach (IMonitor monitor in Monitors)
            {
                monitor.ClearWindows();
            }
        }

        private void AssignWindows()
        {
            IEnumerable<IWindow> windows = Desktop.DetectWindows();

            foreach (IWindow window in windows)
            {
                foreach (IMonitor monitor in Monitors.Where(monitor => monitor.IsInSameScreen(window)))
                {
                    monitor.AddWindow(window);
                }
            }
        }

        public string LogDescription
        {
            get { return "Assigning windows to monitors"; }
        }
    }
}

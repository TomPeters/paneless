using System.Collections.Generic;
using System.Linq;

namespace Paneless.Core.Commands
{
    public class AssignWindowsToMonitorsCommand : Command
    {
        public override void Execute()
        {
            ClearWindows();
            AssignWindows();
        }

        private void ClearWindows()
        {
            foreach (IMonitor monitor in Context.Monitors)
            {
                monitor.ClearWindows();
            }
        }

        private void AssignWindows()
        {
            IEnumerable<IWindow> windows = Context.Desktop.DetectWindows();

            foreach (IWindow window in windows)
            {
                foreach (IMonitor monitor in Context.Monitors.Where(monitor => monitor.IsInSameScreen(window)))
                {
                    monitor.AddWindow(window);
                }
            }
        }
    }
}

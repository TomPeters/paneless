using System.Collections.Generic;
using Paneless.Common;

namespace Paneless.Core.Commands
{
    public class AssignNewWindowToTagCommand : ICommand, ILoggable
    {
        private IWindow NewWindow { get; set; }
        private IEnumerable<IMonitor> Monitors { get; set; }
        private IDesktop Desktop { get; set; }

        public AssignNewWindowToTagCommand(IWindow newWindow, IEnumerable<IMonitor> monitors, IDesktop desktop)
        {
            NewWindow = newWindow;
            Monitors = monitors;
            Desktop = desktop;
        }

        public void Execute()
        {
            bool result = Desktop.IsManagingWindow(NewWindow);
            Desktop.AddWindow(NewWindow);
            foreach (IMonitor monitor in Monitors)
            {
                if (monitor.IsInSameScreen(NewWindow))
                    monitor.AddWindow(NewWindow);
            }
        }

        public string LogDescription
        {
            get
            {
                return "Detected new window: " + NewWindow.Name +
                       "; and its tileability is: " + NewWindow.IsTileable() +
                       "; and its visibility is: " + NewWindow.IsVisible();
            }
        }
    }
}

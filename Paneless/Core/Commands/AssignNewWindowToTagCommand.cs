using System;
using System.Collections.Generic;
using Paneless.Common;

namespace Paneless.Core.Commands
{
    public class AssignNewWindowToTagCommand : ICommand, ILoggable
    {
        private IWindow NewWindow { get; set; }
        private IEnumerable<IMonitor> Monitors { get; set; }

        public AssignNewWindowToTagCommand(IWindow newWindow, IEnumerable<IMonitor> monitors)
        {
            NewWindow = newWindow;
            Monitors = monitors;
        }

        public void Execute()
        {
            if (NewWindow.IsTileable())
            {
                foreach (IMonitor monitor in Monitors)
                {
                    if (monitor.IsInSameScreen(NewWindow))
                        monitor.AddWindow(NewWindow);
                }
            }
        }

        public string LogDescription
        {
            get { return "Detected new window: " + NewWindow.Name + "; and its tileability is: " + NewWindow.IsTileable(); }
        }
    }
}

using System.Collections.Generic;
using Paneless.Core;

namespace Paneless
{
    // Orchestrates Core
    public class Controller
    {
        public Controller()
            : this(new Desktop())
        {
        }

        public Controller(IDesktop desktop)
        {
            Desktop = desktop;
        }

        private IDesktop Desktop { get; set; }

        public void SetDefaultLayouts()
        {
            ITag tag1 = new Tag("1");
            ITag tag2 = new Tag("2");
            Desktop.AddTag(tag1);
            Desktop.AddTag(tag2);

            Desktop.Monitors[0].Tag = tag1;
            Desktop.Monitors[1].Tag = tag2;
        }

        public void AssignWindows()
        {
            List<IWindow> windows = Desktop.DetectWindows();
            foreach (IWindow window in windows)
            {
                foreach (IMonitor monitor in Desktop.Monitors)
                {
                    if (window.Screen.Equals(monitor.Screen))
                    {
                        monitor.AddWindow(window);
                    }
                }
            }
        }
    }
}

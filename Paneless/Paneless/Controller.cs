using System.Collections.Generic;
using Paneless.Layouts;
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
            SetDefaultLayouts();
            AssignWindows();
            SetInitialLayouts();
        }

        private void SetInitialLayouts()
        {
            foreach (IMonitor monitor in Desktop.Monitors)
            {
                monitor.Tag.SetLayout(new CompositeHorizontalLayout());
            }
        }

        public IDesktop Desktop { get; set; }

        private void SetDefaultLayouts() //TODO this is temporary and should be put into some kind of settings/config
        {
            ITag tag1 = new Tag();
            ITag tag2 = new Tag();
            Desktop.AddTag(tag1);
            Desktop.AddTag(tag2);

            Desktop.Monitors[0].Tag = tag1;
            Desktop.Monitors[1].Tag = tag2;
        }

        private void AssignWindows() // TODO: This should always be done at start up (is this the same as controller construction?)
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

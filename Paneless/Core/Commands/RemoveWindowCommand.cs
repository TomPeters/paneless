using System.Collections.Generic;
using Paneless.Common;

namespace Paneless.Core.Commands
{
    public class RemoveWindowCommand : ICommand, ILoggable
    {
        private IWindow Window { get; set; }
        private IDesktop Desktop { get; set; }
        private IEnumerable<ITag> ActiveTags { get; set; }

        public RemoveWindowCommand(IWindow window, IDesktop desktop, IEnumerable<ITag> activeTags)
        {
            Window = window;
            Desktop = desktop;
            ActiveTags = activeTags;
        }

        public void Execute()
        {
            Desktop.RemoveWindow(Window);
            foreach (ITag tag in ActiveTags)
                tag.RemoveWindow(Window);
        }

        public string LogDescription 
        { 
            get { return "Window being removed: " + Window.Name; } 
        }
    }
}

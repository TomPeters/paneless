using System.Collections.Generic;

namespace Paneless.Core.Commands
{
    public class RemoveWindowCommand : ICommand
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
    }
}

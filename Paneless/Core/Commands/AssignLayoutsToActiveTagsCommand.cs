using System.Collections.Generic;
using Paneless.Common;
using Paneless.Core.Layouts;

namespace Paneless.Core.Commands
{
    public class AssignLayoutsToActiveTagsCommand : ICommand, ILoggable
    {
        public AssignLayoutsToActiveTagsCommand(IEnumerable<ITag> activeTags, ILayout layout)
        {
            Layout = layout;
            ActiveTags = activeTags;
        }

        private IEnumerable<ITag> ActiveTags { get; set; }

        private ILayout Layout { get; set; }

        public void Execute()
        {
            foreach (ITag tag in ActiveTags)
            {
                tag.SetLayout(Layout);
            }
        }

        public string LogDescription
        {
            get { return "Assigning layout " + Layout.GetType() + " to tags"; }
        }
    }
}

using System.Collections.Generic;
using Paneless.Common;

namespace Paneless.Core.Commands
{
    public class RefreshWindowPositionsCommand : ICommand, ILoggable
    {
        private IEnumerable<ITag> ActiveTags { get; set; }

        public RefreshWindowPositionsCommand(IEnumerable<ITag> activeTags)
        {
            ActiveTags = activeTags;
        }

        public void Execute()
        {
            foreach (ITag tag in ActiveTags)
            {
                tag.Tile();
            }
        }

        public string LogDescription
        {
            get { return "Retiling"; }
        }
    }
}

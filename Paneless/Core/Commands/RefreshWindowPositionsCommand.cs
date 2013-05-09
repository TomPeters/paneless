using Paneless.Common;

namespace Paneless.Core.Commands
{
    public class RefreshWindowPositionsCommand : Command, ILoggable
    {
        public override void Execute()
        {
            foreach (ITag tag in Context.ActiveTags)
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

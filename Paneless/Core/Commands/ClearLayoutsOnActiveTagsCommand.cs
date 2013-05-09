namespace Paneless.Core.Commands
{
    public class ClearLayoutsOnActiveTagsCommand : Command
    {
        public override void Execute()
        {
            foreach (ITag tag in Context.ActiveTags)
            {
                tag.SetLayout(Context.Desktop.LayoutFactory.CreateLayout("EmptyLayout"));
            }
        }
    }
}

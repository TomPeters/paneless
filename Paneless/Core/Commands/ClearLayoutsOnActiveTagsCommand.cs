namespace Paneless.Core.Commands
{
    public class ClearLayoutsOnActiveTagsCommand : Command
    {
        public override void Execute()
        {
            foreach (ITag tag in DomainObjectProvider.ActiveTags)
            {
                tag.SetLayout(DomainObjectProvider.Desktop.LayoutFactory.CreateLayout("EmptyLayout"));
            }
        }
    }
}

namespace Paneless.Core.Commands
{
    public class AssignLayoutsToActiveTagsCommand : Command
    {
        public override void Execute()
        {
            foreach (ITag tag in Context.ActiveTags)
            {
                tag.SetLayout(Context.Desktop.LayoutFactory.CreateLayout(string.Empty));
            }
        }
    }
}

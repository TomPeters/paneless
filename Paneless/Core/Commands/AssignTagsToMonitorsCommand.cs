namespace Paneless.Core.Commands
{
    public class AssignTagsToMonitorsCommand : Command
    {
        public override void Execute()
        {
            foreach (IMonitor monitor in Context.Monitors)
            {
                ITag newTag = new Tag();
                Context.Desktop.AddTag(newTag);
                monitor.Tag = newTag;
            }
        }
    }
}

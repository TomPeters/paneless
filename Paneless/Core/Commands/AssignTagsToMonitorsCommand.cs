namespace Paneless.Core.Commands
{
    public class AssignTagsToMonitorsCommand : Command
    {
        public override void Execute()
        {
            foreach (IMonitor monitor in DomainObjectProvider.Monitors)
            {
                ITag newTag = new Tag();
                DomainObjectProvider.Desktop.AddTag(newTag);
                monitor.Tag = newTag;
            }
        }
    }
}

using Paneless.Core.Config;
using Paneless.Core.Events;

namespace Paneless.Core.Commands
{
    public class AssignLayoutsToActiveTagsCommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(IEventArguments eventArguments)
        {
            IDomainObjectProvider domainObjectProvider = eventArguments.DomainObjectProvider;
            Configuration config = eventArguments.ConfigurationProvider.Configuration;

            return new AssignLayoutsToActiveTagsCommand(domainObjectProvider.ActiveTags, 
                domainObjectProvider.Desktop.LayoutFactory.CreateLayout(config.Options.DefaultLayout));
        }
    }
}

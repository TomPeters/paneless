using System.Collections.Generic;
using System.Linq;

namespace Paneless.Core
{
    public class DomainObjectProvider : IDomainObjectProvider
    {
        public DomainObjectProvider(IDesktop desktop, IWindowFactory windowFactory)
        {
            Desktop = desktop;
            WindowFactory = windowFactory;
        }

        public IDesktop Desktop { get; private set; }
        public IWindowFactory WindowFactory { get; private set; }

        public IEnumerable<IMonitor> Monitors
        {
            get { return Desktop.Monitors; }
        }

        public IEnumerable<ITag> ActiveTags
        {
            get { return Monitors.Select(m => m.Tag); }
        }
    }

    public interface IDomainObjectProvider
    {
        IDesktop Desktop { get; }
        IWindowFactory WindowFactory { get; }
        IEnumerable<IMonitor> Monitors { get; }
        IEnumerable<ITag> ActiveTags { get; }
    }
}

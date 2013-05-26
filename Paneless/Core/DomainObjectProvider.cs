using System.Collections.Generic;
using System.Linq;

namespace Paneless.Core
{
    public class DomainObjectProvider : IDomainObjectProvider
    {
        public DomainObjectProvider(IDesktop desktop)
        {
            Desktop = desktop;
        }

        public IDesktop Desktop { get; private set; }

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
        IEnumerable<IMonitor> Monitors { get; }
        IEnumerable<ITag> ActiveTags { get; }
    }
}

using System.Collections.Generic;

namespace Paneless.Core.Config
{
    public class KeyBinding
    {
        public IEnumerable<string> Keys { get; set; }
        public CommandConfiguration Action { get; set; }
    }
}

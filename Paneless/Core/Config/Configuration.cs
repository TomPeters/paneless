using System.Collections.Generic;

namespace Paneless.Core.Config
{
    public class Configuration
    {
        public IEnumerable<KeyBinding> KeyBindings { get; set; }
        public Options Options { get; set; }
    }
}

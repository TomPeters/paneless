using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paneless.Core
{
    // Combination of windows and how to lay them out (template). This contains similar structure to tags but contains different information and has a different purpose

    public class Layout : ILayout
    {
        public ITemplate Template { get; set; }
    }

    public interface ILayout
    {
        ITemplate Template { get; }
    }
}

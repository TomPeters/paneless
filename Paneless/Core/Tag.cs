using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paneless.Core
{
    // A collection of windows and it'name associated layout. Windows do not have to be unique between tags. Has a name etc. Can be a child of a monitor
    public class Tag : ITag
    {
        private readonly List<IWindow> _windows; 

        public Tag(string name)
        {
            _windows = new List<IWindow>();
            Name = name;
        }

        public string Name { get; set; }
        public List<IWindow> Windows { get { return _windows; } }
        public void AddWindow(IWindow window)
        {
            _windows.Add(window);
        }

        public ILayout Layout { get; set; }
    }

    public interface ITag
    {
        string Name { get; set; }
        List<IWindow> Windows { get; }
        void AddWindow(IWindow window);
        ILayout Layout { get; set; }
    }
}

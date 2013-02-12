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
        private ILayout _layout;

        public Tag()
        {
            _windows = new List<IWindow>();
        }

        public List<IWindow> Windows { get { return _windows; } }

        public Rectangle Domain { get; set; }

        public void AddWindow(IWindow window)
        {
            _windows.Add(window);
        }

        public void SetLayout(ILayout newLayout)
        {
            _layout = newLayout;
            foreach (IWindow window in Windows)
            {
                _layout.AddWindowsWithoutTile(window);
            }
            _layout.Domain = Domain;
            _layout.Tile();
        }
    }

    public interface ITag
    {
        List<IWindow> Windows { get; }
        Rectangle Domain { get; set; }
        void AddWindow(IWindow window);
        void SetLayout(ILayout newLayout);
    }
}

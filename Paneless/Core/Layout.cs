using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paneless.Core
{
    // Combination of windows and how to lay them out (template). This contains similar structure to tags but contains different information and has a different purpose

    public abstract class LayoutBase : ILayout
    {
        private readonly List<IWindow> _windows;

        protected LayoutBase()
        {
            _windows = new List<IWindow>();
        }

        public IEnumerable<IWindow> Windows { get { return _windows; } }

        public Rectangle Domain { get; set; }

        public void AddWindow(IWindow window)
        {
            AddWindowsWithoutTile(window);
            Tile();
        }

        public void AddWindowsWithoutTile(IWindow window)
        {
            _windows.Add(window);
        }

        public abstract void Tile();
    }

    public interface ILayout
    {
        void Tile();
        IEnumerable<IWindow> Windows { get; }
        Rectangle Domain { get; set; }
        void AddWindow(IWindow window);
        void AddWindowsWithoutTile(IWindow window);
    }
}

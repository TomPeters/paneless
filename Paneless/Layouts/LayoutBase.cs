﻿using System.Collections.Generic;
using Paneless.Core;

namespace Paneless.Layouts
{
    // Combination of windows and how to lay them out (template). This contains similar structure to tags but contains different information and has a different purpose

    public abstract class LayoutBase : ILayout
    {
        private readonly List<IWindow> _windows;

        protected LayoutBase()
        {
            _windows = new List<IWindow>();
        }

        public IList<IWindow> Windows { get { return _windows; } }

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

        public void RenderInDomain(IWindow window, Rectangle windowDomain)
        {
            FullDomainLayout fullDomainLayout = new FullDomainLayout(0) {Domain = windowDomain}; //TODO Need a better way of adding a default border width than hard coding it here
            fullDomainLayout.AddWindow(window);
        }
    }
}

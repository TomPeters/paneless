using System.Collections.Generic;
using System.Linq;
using Paneless.Core.Layouts;

namespace Paneless.Core
{
    // A collection of windows and it's associated layout. Windows do not have to be unique between tags. Can be a property of a monitor
    public class Tag : ITag
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly List<IWindow> _windows;
        private ILayout _layout;

        public Tag()
        {
            _windows = new List<IWindow>();
            _layout = new EmptyLayout();
        }

        public List<IWindow> Windows { get { return _windows; } }

        public Rectangle Domain { get; set; }

        public void AddWindow(IWindow window)
        {
            Logger.Debug("Window " + window.Name + " added to Tag " + this);
            _windows.Add(window);
        }

        public void ClearWindows()
        {
            _windows.Clear();
        }

        public void SetLayout(ILayout newLayout)
        {
            Logger.Debug("Tag " + this + " Layout has changed to " + newLayout.GetType());
            _layout = newLayout;
        }

        public void Tile()
        {
            Logger.Debug("Tag " + this + " is being retiled");
            _layout.ClearWindows();
            foreach (IWindow window in Windows)
            {
                _layout.AddWindowsWithoutTile(window);
            }
            _layout.Domain = Domain;
            _layout.Tile();
        }

        public void RemoveWindow(IWindow window)
        {
            if (_windows.Any(w => w.Equals(window)))
                _windows.Remove(_windows.Single(w => w.Equals(window)));
        }
    }

    public interface ITag
    {
        List<IWindow> Windows { get; }
        Rectangle Domain { get; set; }
        void AddWindow(IWindow window);
        void SetLayout(ILayout newLayout);
        void ClearWindows();
        void Tile();
        void RemoveWindow(IWindow window);
    }
}

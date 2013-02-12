using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Paneless.Core
{
    // A combination of screen (physical device) and its layout (windows and how to tile them)
    public class Monitor : IMonitor
    {
        private ITag _tag;

        public Screen Screen { get; set; }
        public ITag Tag { get { return _tag; } set
        {
            _tag = value;
            _tag.Domain = new Rectangle(Screen.WorkingArea);
        } }

        public void AddWindow(IWindow window)
        {
            Tag.AddWindow(window);
        }
    }

    public interface IMonitor
    {
        Screen Screen { get; set; }
        ITag Tag { get; set; }
        void AddWindow(IWindow window);
    }
}

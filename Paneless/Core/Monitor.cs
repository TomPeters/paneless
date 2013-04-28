using System.Windows.Forms;

namespace Paneless.Core
{
    // A combination of screen (physical device) and its layout (windows and how to tile them)
    public class Monitor : IMonitor
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ITag _tag;

        public Screen Screen { private get; set; }

        public bool IsInSameScreen(IWindow window)
        {
            return Screen.Equals(window.Screen);
        }

        public ITag Tag 
        { 
            get
            {
                return _tag;
            } 
            set
            {
                _tag = value;
                _tag.Domain = new Rectangle(Screen.WorkingArea);
            } 
        }

        public void AddWindow(IWindow window)
        {
            Logger.Debug("Window " + window.Name + " Added to Monitor " + Screen);
            Tag.AddWindow(window);
        }

        public void ClearWindows()
        {
            Tag.ClearWindows();
        }
    }

    public interface IMonitor
    {
        bool IsInSameScreen(IWindow window);
        ITag Tag { get; set; }
        void AddWindow(IWindow window);
        void ClearWindows();
    }
}

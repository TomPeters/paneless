using System.Windows.Forms;
using Layouts;
using Paneless.Core;
using WinApi.Interface;
using WinApi.Windows7;

namespace Paneless.Launcher
{
    public static class Launcher
    {
        static void Main(string[] args)
        {
            IWindowManager windowManager = new WindowManager();
            IDesktopManager desktopManager = new DesktopManager();
            PanelessApplicationContext applicationContext = new PanelessApplicationContext(new Controller(new LayoutFactory(), new Desktop(desktopManager, windowManager), desktopManager, windowManager));
            Application.Run(applicationContext);
        }
    }
}

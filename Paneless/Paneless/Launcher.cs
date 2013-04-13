using System.Windows.Forms;
using Layouts;

namespace Paneless.Launcher
{
    public static class Launcher
    {
        static void Main(string[] args)
        {
            PanelessApplicationContext applicationContext = new PanelessApplicationContext(new Controller(new LayoutFactory()));
            Application.Run(applicationContext);
        }
    }
}

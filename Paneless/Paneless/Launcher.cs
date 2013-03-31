using System.Threading;
using System.Windows.Forms;

namespace Paneless.Launcher
{
    public static class Launcher
    {
        static void Main(string[] args)
        {
            
            //The start up process needs to change to be:
            //1) Check that paneless is not currently running
            //2) Initialize paneless (create forms etc) including the hidden form
            //3) Call the SetupHookReturnWindow(HWND hWnd) function to set the hwnd for the hook window (this can be moved out of the dlls and into an interop method)
            //4) Initialise a seperate process which is a hook setup executor. This should be run in 32-bit. It uses the 32-bit dll to inject hooks into other 32-bit processes
            //5) If we are running 64-bit then do the same thing as in step 4 but run in 64-bit.
            PanelessApplicationContext applicationContext = new PanelessApplicationContext();
            Application.Run(applicationContext);
        }
    }
}

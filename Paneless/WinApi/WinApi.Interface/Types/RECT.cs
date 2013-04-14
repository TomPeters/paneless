using System.Runtime.InteropServices;

namespace WinApi.Interface.Types
{
    //http://www.pinvoke.net/default.aspx/Structures/RECT.html
    [StructLayout(LayoutKind.Sequential)] // Needs to be in the correct order for use by user32.dll
    public class RECT
    {
            public int Left, Top, Right, Bottom;
    }
}

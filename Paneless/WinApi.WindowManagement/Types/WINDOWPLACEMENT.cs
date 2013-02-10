using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paneless.WinApi
{
    //http://msdn.microsoft.com/en-us/library/ms632611(v=vs.85).aspx
    public struct WINDOWPLACEMENT
    {
        public int length;
        public int flags;
        public int showCmd;
        public System.Drawing.Point ptMinPosition;
        public System.Drawing.Point ptMaxPosition;
        public System.Drawing.Rectangle rcNormalPosition;
    }
}

using Paneless.WinApi;

namespace Paneless.Core
{
    public class WindowLocation
    {
        public WindowLocation(RECT rect)
        {
            Left = rect.Left;
            Right = rect.Right;
            Top = rect.Top;
            Bottom = rect.Bottom;
        }

        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }

        public int X { get { return Left; } }
        public int Y { get { return Top; } }

        public int Width { get { return Right - Left; } }
        public int Height { get { return Bottom - Top; } }
    }
}

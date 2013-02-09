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

        private WindowLocation()
        {
        }

        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }

        public int X { get { return Left; } }
        public int Y { get { return Top; } }

        public int Width { get { return Right - Left; } }
        public int Height { get { return Bottom - Top; } }

        public RECT GetRect()
        {
            return new RECT {Left = Left, Right = Right, Top = Top, Bottom = Bottom};
        }

        public WindowLocation Clone()
        {
            WindowLocation clone = new WindowLocation();
            clone.Left = Left;
            clone.Right = Right;
            clone.Top = Top;
            clone.Bottom = Bottom;
            return clone;
        }
    }
}

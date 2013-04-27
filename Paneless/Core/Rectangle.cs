using System;
using WinApi.Interface.Types;

namespace Paneless.Core
{
    public class Rectangle : IEquatable<Rectangle>
    {
        public Rectangle(RECT rect)
        {
            Left = rect.Left;
            Right = rect.Right;
            Top = rect.Top;
            Bottom = rect.Bottom;
        }

        public Rectangle(System.Drawing.Rectangle workingArea)
        {
            Left = workingArea.Left;
            Right = workingArea.Right;
            Top = workingArea.Top;
            Bottom = workingArea.Bottom;
        }

        public Rectangle()
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

        public Rectangle Clone()
        {
            Rectangle clone = new Rectangle
                {
                    Left = Left, 
                    Right = Right, 
                    Top = Top, 
                    Bottom = Bottom
                };
            return clone;
        }

        public bool Equals(Rectangle other)
        {
            return (Left == other.Left) 
                && (Right == other.Right)
                && (Top == other.Top)
                && (Bottom == other.Bottom);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paneless.WinApi;
using EasyAssertions;

namespace Paneless.Core.UnitTests
{
    [TestClass]
    public class WindowLocationTests
    {
        private const int LEFT = 1;
        private const int RIGHT = 2;
        private const int TOP = 3;
        private const int BOTTOM = 4;

        [TestMethod]
        public void TestPoints()
        {
            RECT rect = GetRect();
            WindowLocation sut = new WindowLocation(rect);
            sut.Left.ShouldBe(LEFT);
            sut.Right.ShouldBe(RIGHT);
            sut.Top.ShouldBe(TOP);
            sut.Bottom.ShouldBe(BOTTOM);
            sut.X.ShouldBe(LEFT);
            sut.Y.ShouldBe(TOP);
            sut.Width.ShouldBe(RIGHT - LEFT);
            sut.Height.ShouldBe(BOTTOM - TOP);
        }

        private static RECT GetRect()
        {
            RECT rect = new RECT();
            rect.Left = LEFT;
            rect.Right = RIGHT;
            rect.Top = TOP;
            rect.Bottom = BOTTOM;
            return rect;
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyAssertions;
using WinApi.Interface.Types;

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
            Rectangle sut = new Rectangle(rect);
            sut.Left.ShouldBe(LEFT);
            sut.Right.ShouldBe(RIGHT);
            sut.Top.ShouldBe(TOP);
            sut.Bottom.ShouldBe(BOTTOM);
            sut.X.ShouldBe(LEFT);
            sut.Y.ShouldBe(TOP);
            sut.Width.ShouldBe(RIGHT - LEFT);
            sut.Height.ShouldBe(BOTTOM - TOP);
        }

        [TestMethod]
        public void TestClone()
        {
            Rectangle original = new Rectangle(GetRect());
            Rectangle clone = original.Clone();
            clone.Left += 1;
            clone.Right += 1;
            clone.Top += 1;
            clone.Bottom += 1;
            clone.Left.ShouldBe(original.Left + 1);
            clone.Right.ShouldBe(original.Right + 1);
            clone.Top.ShouldBe(original.Top + 1);
            clone.Bottom.ShouldBe(original.Bottom + 1);
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

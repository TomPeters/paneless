using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paneless.Core.Events;
using EasyAssertions;

namespace Paneless.Core.UnitTests.Events
{
    [TestClass]
    public class KeyEventTests
    {
        [TestMethod]
        public void TwoKeyEventsOfSameKeyComboAreEqual()
        {
            new KeyEvent(HotkeyEvents.Tile).Equals(new KeyEvent(HotkeyEvents.Tile)).ShouldBe(true);
        }

        [TestMethod]
        public void TwoKeyEventsOfDifferentKeyComboAreNotEqual()
        {
            new KeyEvent(HotkeyEvents.Tile).Equals(new KeyEvent(HotkeyEvents.Untile)).ShouldBe(false);
        }
    }
}

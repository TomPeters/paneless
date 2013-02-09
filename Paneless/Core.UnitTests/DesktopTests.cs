using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using EasyAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.UnitTests
{
    [TestClass]
    public class DesktopTests
    {
        [TestMethod]
        public void TestPopulateScrens()
        {
            Desktop sut = new Desktop();
            sut.PopulateScreens();
            sut.Screens.ShouldNotBeNull().And.Count().ShouldBeGreaterThan(0); // Environment dependent (can't be headless)
        }
    }
}

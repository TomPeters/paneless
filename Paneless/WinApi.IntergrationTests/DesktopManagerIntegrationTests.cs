using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paneless.WinApi;
using EasyAssertions;

namespace WinApi.IntergrationTests
{
    [TestClass]
    public class DesktopManagerIntegrationTests
    {
        private IDesktopManager _dm;
        private int _numEnumWindows;

        [TestInitialize]
        public void Setup()
        {
            _dm = new DesktopManager();
        }

        [TestMethod]
        public void EnumWindows()
        {
            _numEnumWindows = 0;
            WindowsEnumProcess callback = WindowsEnumProcessCallback;
            _dm.EnumWindows(callback);

            _numEnumWindows.ShouldBeGreaterThan(0);
        }

        private bool WindowsEnumProcessCallback(int hWnd, int lParam)
        {
            _numEnumWindows++;
            return true;
        }

        [TestMethod]
        public void RegisterWindowMessage()
        {
            _dm.RegisterWindowMessage("PANELESS_TEST_MESSAGE").ShouldNotBe(0);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyAssertions;
using WinApi.Interface;
using WinApi.Windows7;

namespace WinApi.IntergrationTests
{
    [TestClass]
    public abstract class DesktopManagerIntegrationTests
    {
        private IDesktopManager _dm;
        private int _numEnumWindows;

        protected abstract IDesktopManager DesktopManager { get; }

        [TestInitialize]
        public void Setup()
        {
            _dm = DesktopManager;
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

    [TestClass]
    public class Windows7DesktopManagerIntegrationTests : DesktopManagerIntegrationTests
    {
        protected override IDesktopManager DesktopManager
        {
            get { return new DesktopManager(new NamedPipeClient()); }
        }
    }
}

using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Paneless.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyAssertions;
using Paneless.WinApi;

namespace Paneless.IntegrationTests
{
    [TestClass]
    public class NotepadWindowIntegrationTests
    {
        private const string TestWindowName = "Untitled - Notepad";
        private Process _process;
        private IWindow _sut;

        [TestInitialize]
        public void Setup()
        {
            _process = Process.Start("Notepad.exe");
            Thread.Sleep(1000);
            _sut = new Window(TestWindowName);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _process.CloseMainWindow();
            _process.Close();
        }

        [TestMethod]
        public void GetPropertiesTest()
        {
            _sut.Name.ShouldBe(TestWindowName);
            _sut.ClassName.ShouldBe("Notepad");
            WindowLocation location = _sut.Location;
            location.Left.ShouldNotBeNull();
            location.Right.ShouldNotBeNull();
            location.Top.ShouldNotBeNull();
            location.Bottom.ShouldNotBeNull();
            _sut.State.ShouldBe(ShowState.SW_SHOWNORMAL);
            _sut.ExtendedWindowStyleFlags.HasFlag(ExtendedWindowStyleFlags.WS_EX_LEFT);
            _sut.Screen.ShouldBeA<Screen>();
        }

        [TestMethod]
        public void RepositionWindowTest()
        {
            WindowLocation originalLocation = _sut.Location;
            WindowLocation proposedLocation = originalLocation.Clone();
            proposedLocation.Top -= 1;
            proposedLocation.Bottom += 1;
            proposedLocation.Left -= 1;
            proposedLocation.Right += 1;
            _sut.SetLocation(proposedLocation);

            WindowLocation newLocation = _sut.Location;

            newLocation.Left.ShouldBe(proposedLocation.Left);
            newLocation.Right.ShouldBe(proposedLocation.Right);
            newLocation.Top.ShouldBe(proposedLocation.Top);
            newLocation.Bottom.ShouldBe(proposedLocation.Bottom);
        }
    }
}

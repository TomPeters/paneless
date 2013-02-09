using System.Diagnostics;
using System.Threading;
using Paneless.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyAssertions;

namespace Paneless.IntegrationTests
{
    [TestClass]
    public class NotepadWindowIntegrationTests
    {
        private Process _process;

        [TestInitialize]
        public void Setup()
        {
            _process = Process.Start("Notepad.exe");
            Thread.Sleep(1000);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _process.CloseMainWindow();
            _process.Close();
        }

        [TestMethod]
        public void RetrieveName()
        {
            const string testWindowName = "Untitled - Notepad";
            IWindow sut = new Window(testWindowName);
            sut.Name.ShouldBe(testWindowName);
            sut.ClassName.ShouldBe("Notepad");
            WindowLocation location = sut.Location;
            location.Left.ShouldNotBeNull();
            location.Right.ShouldNotBeNull();
            location.Top.ShouldNotBeNull();
            location.Bottom.ShouldNotBeNull();
        }
    }
}

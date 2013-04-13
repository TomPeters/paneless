using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Paneless
{
    public class PanelessApplicationContext : ApplicationContext
    {
        private NotifyIcon _notifyIcon;
        private readonly IController _controller;
        private Form _hiddenForm;

        public PanelessApplicationContext(IController controller)
        {
            InitializeContext();
            _controller = controller;
            SetupShellHookWindow();
        }

        private void InitializeContext()
        {
            var components = new Container();
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add(new ToolStripButton("Exit", null, exitItem_Click));
            _notifyIcon = new NotifyIcon(components)
                {
                    ContextMenuStrip = contextMenuStrip,
                    Visible = true,
                    Icon = new Icon(Assembly.GetEntryAssembly().GetManifestResourceStream("Paneless.assets.paneless.ico")),
                    Text = "This is my notify icon"
                };
        }

        private void exitItem_Click(object sender, EventArgs e)
        {
            ExitThread();
        }

        protected override void Dispose(bool disposing)
        {
            _controller.TerminateHook();
            _controller.UnregisterHotKeys(_hiddenForm.Handle);
        }

        protected override void ExitThreadCore()
        {
            _notifyIcon.Visible = false;
            base.ExitThreadCore();
        }

        private void SetupShellHookWindow()
        {
            _hiddenForm = new HiddenForm(_controller);
        }
    }
}

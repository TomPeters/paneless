using System.Windows.Forms;

namespace Paneless
{
    public class HiddenForm : Form
    {
        private readonly Form _form;
        private readonly IController _controller;

        public HiddenForm(IController controller)
        {
            _form = new Form {Visible = false, ShowInTaskbar = false};
            _controller = controller;
        }

        protected override void DefWndProc(ref Message m)
        {
            _controller.WndProcEventHandler(m);
            base.DefWndProc(ref m);
        }
    }
}

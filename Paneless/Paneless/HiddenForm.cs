using System.Windows.Forms;
using Paneless.Core.Events;

namespace Paneless
{
    public class HiddenForm : Form
    {
        private const string CustomWindowMessage = "PANELESS_7F75020C-34E7-45B4-A5F8-6827F9DB7DE2";

        private readonly Form _form;

        public HiddenForm(IController controller)
        {
            _form = new Form { Visible = false, ShowInTaskbar = false };
            EventFactory = new EventFactory(controller, SetupEventBinding(controller));
        }

        private IEventFactory EventFactory { get; set; }

        private int SetupEventBinding(IController controller)
        {
            int windowMessage = controller.RegisterWindowMessage(CustomWindowMessage);
            controller.SetupHook(Handle);
            controller.SetupHotkeys(Handle);
            return windowMessage;
        }

        protected override void DefWndProc(ref Message m)
        {
            if(EventFactory != null) // Check that the factory has been created so we don't handle messages before we are ready
                EventFactory.CreateEventFromWindowMessage(m).FireEvent();
            base.DefWndProc(ref m);
        }
    }
}

using System.Windows.Forms;
using Paneless.Core;
using Paneless.Core.Events;
using WinApi.Interface;

namespace Paneless
{
    public class HiddenForm : Form
    {
        private const string CustomWindowMessage = "PANELESS_7F75020C-34E7-45B4-A5F8-6827F9DB7DE2";
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Form _form;

        public HiddenForm(IContextProvider contextProvider, IEventManager eventManager, IWinApiRegistrationManager registrationManager)
        {
            Logger.Debug("Creating hidden form");
            _form = new Form { Visible = false, ShowInTaskbar = false };
            RegistrationManager = registrationManager;
            EventFactory = new EventFactory(contextProvider, SetupEventBinding());
            EventManager = eventManager;
        }

        private IEventFactory EventFactory { get; set; }

        private IEventManager EventManager { get; set; }

        private IWinApiRegistrationManager RegistrationManager { get; set; }

        private int SetupEventBinding()
        {
            int windowMessage = RegistrationManager.RegisterWindowMessage(CustomWindowMessage);
            RegistrationManager.SetupHooks(Handle);
            RegistrationManager.SetupHotKeys(Handle);
            return windowMessage;
        }

        public void TerminateEventBinding()
        {
            RegistrationManager.TerminateHooks();
            RegistrationManager.UnregisterHotKeys(Handle);
        }

        protected override void DefWndProc(ref Message m)
        {
            if(EventFactory != null) // Check that the factory has been created so we don't handle messages before we are ready
                EventManager.TriggerEvent(EventFactory.CreateEvent(m));
            base.DefWndProc(ref m);
        }

        public void TriggerStartupEvent()
        {
            EventManager.TriggerEvent(EventFactory.CreateStartupEvent());
        }

        public void TriggerShutdownEvent()
        {
            EventManager.TriggerEvent(EventFactory.CreateShutdownEvent());
        }
    }
}

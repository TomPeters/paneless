using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Paneless.Core;
using Paneless.Core.Commands;
using Paneless.Core.Config;
using Paneless.Core.Events;
using WinApi.Windows7;

namespace Paneless
{
    public class PanelessApplicationContext : ApplicationContext
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private NotifyIcon _notifyIcon;
        private HiddenForm _hiddenForm;

        public PanelessApplicationContext(IDomainObjectProvider domainObjectProvider, IConfigurationProvider configurationProvider)
        {
            DomainObjectProvider = domainObjectProvider;
            ConfigurationProvider = configurationProvider;
            InitializeContext();
            SetupShellHookWindow();
            _hiddenForm.TriggerStartupEvent();
        }

        private IDomainObjectProvider DomainObjectProvider { get; set; }

        private IConfigurationProvider ConfigurationProvider { get; set; }

        private void InitializeContext()
        {
            Logger.Debug("Setting up the Notify Icon");
            var components = new Container();
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add(new ToolStripButton("Exit", null, exitItem_Click));
            _notifyIcon = new NotifyIcon(components)
                {
                    ContextMenuStrip = contextMenuStrip,
                    Visible = true,
                    Icon = new Icon(Assembly.GetEntryAssembly().GetManifestResourceStream("Paneless.assets.paneless.ico")),
                    Text = "Paneless"
                };
        }

        private void exitItem_Click(object sender, EventArgs e)
        {
            Logger.Info("Exit Button Clicked");
            ExitThread();
        }

        protected override void Dispose(bool disposing)
        {
            // Which order should these calls be in?
            _hiddenForm.TriggerShutdownEvent();
            _hiddenForm.TerminateEventBinding();
        }

        protected override void ExitThreadCore()
        {
            _notifyIcon.Visible = false;
            base.ExitThreadCore();
        }

        private void SetupShellHookWindow()
        {
            _hiddenForm = new HiddenForm(DomainObjectProvider, ConfigurationProvider, new EventManager(new CommandEventFactory()), new WinApiRegistrationManager(new DesktopManager(new NamedPipeClient())));
        }
    }
}

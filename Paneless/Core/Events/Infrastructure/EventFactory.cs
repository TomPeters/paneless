using System;
using System.Windows.Forms;
using Paneless.Core.Config;
using WinApi.Interface.Constants;

namespace Paneless.Core.Events
{
    // TODO: This class need to be tested - but this shouldn't happen until we get better infrastructure in place
    public class EventFactory : IEventFactory
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const int WM_HOTKEY = 0x0312;
        private static int _windowMessage;

        public EventFactory(IDomainObjectProvider domainObjectProvider, IConfigurationProvider configurationProvider, int windowMessage)
        {
            DomainObjectProvider = domainObjectProvider;
            ConfigurationProvider = configurationProvider;
            _windowMessage = windowMessage;
        }

        private IDomainObjectProvider DomainObjectProvider { get; set; }

        private IConfigurationProvider ConfigurationProvider { get; set; }

        public ITriggeredEvent CreateEvent(Message msg)
        {
            IEventBuilder builder = InitializeBuilder();
            AddHotkeyEvents(builder, msg);
            AddWindowMessageEventsAndArguments(builder, msg);
            return builder.Build();
        }

        public ITriggeredEvent CreateStartupEvent()
        {
            IEventBuilder builder = InitializeBuilder();
            builder.AddEvent(new StartupEvent());
            return builder.Build();
        }

        public ITriggeredEvent CreateShutdownEvent()
        {
            IEventBuilder builder = InitializeBuilder();
            builder.AddEvent(new ShutdownEvent());
            return builder.Build();
        }

        private IEventBuilder InitializeBuilder()
        {
            IEventBuilder builder = new EventBuilder();
            builder.Arguments.DomainObjectProvider = DomainObjectProvider;
            builder.Arguments.ConfigurationProvider = ConfigurationProvider;
            return builder;
        }

        private void AddWindowMessageEventsAndArguments(IEventBuilder builder, Message msg)
        {
            if (msg.Msg == _windowMessage) // We are only interested in messages sent from our hooks
            {
                builder.Arguments.Hwnd = msg.WParam;
                IntPtr Message = msg.LParam;
                builder.Arguments.Message = Message;
                WindowNotification messageType = (WindowNotification) Message;
                switch (messageType)
                {
                    case (WindowNotification.WM_MOVING):
                        break;
                    case (WindowNotification.WM_MOVE):
                        builder.AddEvent(new WindowMovedEvent());
                        break;
                    case (WindowNotification.WM_SHOWWINDOW):
                        break;
                    case (WindowNotification.WM_SIZING):
                        builder.AddEvent(new WindowResizingEvent());
                        break;
                    case (WindowNotification.WM_SIZE):
                        builder.AddEvent(new WindowResizedEvent());
                        break;
                    case (WindowNotification.WM_CREATE):
                        builder.AddEvent(new WindowCreationEvent());
                        break;
                }
            }
        }

        private static void AddHotkeyEvents(IEventBuilder builder, Message msg)
        {
            if (msg.Msg == WM_HOTKEY)
            {
                if ((int) msg.WParam == (int) HotkeyEvents.Tile)
                {
                    builder.AddEvent(new KeyEvent(HotkeyEvents.Tile));
                }
                else if ((int) msg.WParam == (int) HotkeyEvents.Untile)
                {
                    builder.AddEvent(new KeyEvent(HotkeyEvents.Untile));
                }
            }
        }
    }

    public interface IEventFactory
    {
        ITriggeredEvent CreateEvent(Message msg);
        ITriggeredEvent CreateStartupEvent();
        ITriggeredEvent CreateShutdownEvent();
    }
}

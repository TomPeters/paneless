using System;
using System.Windows.Forms;
using WinApi.Interface.Constants;

namespace Paneless.Core.Events
{
    public class EventFactory : IEventFactory
    {
        private const int WM_HOTKEY = 0x0312;
        private static int _windowMessage;

        public EventFactory(IController controller, int windowMessage)
            : this(controller)
        {
            _windowMessage = windowMessage;
        }

        private EventFactory(IController controller)
        {
            Controller = controller;
        }

        private IController Controller { get; set; }

        private IEvent EmptyEvent
        {
            get { return new EmptyEvent(); }
        }

        public IEvent CreateEvent(Message msg)
        {
            ILoggableEvent newEvent = CreateEventFromWindowMessage(msg);
            return newEvent == null ? EmptyEvent : new LoggedEvent(newEvent);
        }

        private ILoggableEvent CreateEventFromWindowMessage(Message msg)
        {
            ILoggableEvent newEvent = null;
            if (msg.Msg == WM_HOTKEY) //WM_HOTKEY
            {
                if ((int)msg.WParam == (int)HotkeyEvents.Tile)
                {
                    newEvent = new ChangeLayout(Controller, "");
                }
                else if ((int)msg.WParam == (int)HotkeyEvents.Untile)
                {
                    newEvent = new ChangeLayout(Controller, "EmptyLayout");
                }
            }
            else if (msg.Msg == _windowMessage) // We are only interested in messages sent from our hooks
            {
                IntPtr HWnd = msg.WParam;
                IntPtr Message = msg.LParam;
                WindowNotification messageType = (WindowNotification) Message;
                switch (messageType)
                {
                    case (WindowNotification.WM_MOVING):
                        break;
                    case (WindowNotification.WM_MOVE):
                        newEvent = new RefreshAllTags(Controller);
                        break;
                    case (WindowNotification.WM_SHOWWINDOW):
                        break;
                    case (WindowNotification.WM_SIZING):
                    case (WindowNotification.WM_SIZE):
                        newEvent = new RefreshAllTags(Controller);
                        break;
                }
            }
            return newEvent;
        }
    }

    public interface IEventFactory
    {
        IEvent CreateEvent(Message msg);
    }
}

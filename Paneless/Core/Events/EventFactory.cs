using System;
using System.Windows.Forms;
using Paneless.WinApi.Constants;

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

        private Event EmptyEvent
        {
            get { return new EmptyEvent(Controller); }
        }

        public void CreateEventFromWindowMessage(Message msg)
        {
            Event newEvent = EmptyEvent;
            if (msg.Msg == WM_HOTKEY) //WM_HOTKEY
            {
                if ((int)msg.WParam == (int)HotkeyEvents.Tile)
                {
                    newEvent = new TileWindows(Controller);
                }
            }
            if (msg.Msg == _windowMessage) // We are only interested in messages sent from our hooks
            {
                IntPtr HWnd = msg.WParam;
                IntPtr Message = msg.LParam;
                WindowNotification messageType = (WindowNotification)Message;
                switch (messageType)
                {
                    case (WindowNotification.WM_MOVING):
                        // Trigger event wm_moving with argument HWnd
                        break;
                    case (WindowNotification.WM_SHOWWINDOW):
                        // Trigger event WM_Showwindow with argument HWnd
                        break;
                    default:
                        // Unhandled event - do nothing
                        break;
                }
            }
            newEvent.FireEvent();
        }
    }

    public interface IEventFactory
    {
        void CreateEventFromWindowMessage(Message msg);
    }
}

namespace Paneless.Core.Events
{
    public class KeyEvent : Event<KeyEvent>
    {
        private readonly HotkeyEvents _keyEventType;

        public KeyEvent(HotkeyEvents keyEventType)
        {
            _keyEventType = keyEventType;
        }

        protected override bool Equals(KeyEvent other)
        {
            return other._keyEventType == _keyEventType;
        }
    }
}

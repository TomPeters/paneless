namespace Paneless.Core.Events
{
    public class ChangeLayout : Event
    {
        private string _layout;

        public ChangeLayout(IController controller, string layout)
            : base(controller)
        {
            _layout = layout;
        }

        public override void FireEvent()
        {
            Controller.SetLayouts("");
        }
    }
}

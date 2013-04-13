namespace Paneless.Core.Events
{
    public class TileWindows : Event
    {
        public TileWindows(IController controller)
            : base(controller)
        {
            FireEvent();
        }

        public override void PerformAction()
        {
            Controller.SetLayouts(Controller.DefaultLayout);
        }
    }
}

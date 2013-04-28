using System;

namespace Paneless.Core.Events
{
    class RefreshAllTags : Event
    {
        public RefreshAllTags(IController controller) 
            : base(controller)
        {
        }

        public override void FireEvent()
        {
            Controller.RefreshAllTags();
        }

        public override string LogDescription
        {
            get { return "Refreshing/Re-tiling all tags"; }
        }
    }
}

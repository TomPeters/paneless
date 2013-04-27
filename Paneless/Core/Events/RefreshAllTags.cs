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
    }
}

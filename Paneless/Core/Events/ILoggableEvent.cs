using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paneless.Core.Events
{
    internal interface ILoggableEvent : IEvent
    {
        string LogDescription { get; }
    }
}

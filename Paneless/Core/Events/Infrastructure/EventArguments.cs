using System;
using Paneless.Core.Commands;

namespace Paneless.Core.Events
{
    public class EventArguments : IEventArguments
    {
        public IDomainObjectProvider DomainObjectProvider { get; set; }
        public IntPtr Hwnd { get; set; }
        public IntPtr Message { get; set; }
    }

    public interface IEventArguments
    {
        IDomainObjectProvider DomainObjectProvider { get; set; }
        IntPtr Hwnd { get; set; }
        IntPtr Message { get; set; }
    }
}

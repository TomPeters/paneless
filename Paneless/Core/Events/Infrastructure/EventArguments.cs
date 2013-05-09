using System;
using Paneless.Core.Commands;

namespace Paneless.Core.Events
{
    public class EventArguments : IEventArguments
    {
        public IContextProvider ContextProvider { get; set; }
        public IntPtr Hwnd { get; set; }
        public IntPtr Message { get; set; }
    }

    public interface IEventArguments
    {
        IContextProvider ContextProvider { get; set; }
        IntPtr Hwnd { get; set; }
        IntPtr Message { get; set; }
    }
}

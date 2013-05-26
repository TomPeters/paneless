using Paneless.Common;
using Paneless.Core.Events;

namespace Paneless.Core.Commands
{
    public class LoggedCommandProxy : ICommand
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ICommand WrappedCommand { get; set; }

        public LoggedCommandProxy(ICommand wrappedCommand)
        {
            WrappedCommand = wrappedCommand;
        }

        public void Execute()
        {
            ILoggable loggableCommand = WrappedCommand as ILoggable;
            if (loggableCommand != null)
                Logger.Debug("Performing Command: " + loggableCommand.LogDescription);
            WrappedCommand.Execute();
        }
    }
}

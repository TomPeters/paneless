using System;
using System.IO.Pipes;

namespace WinApi.Windows7
{
    public interface INamedPipeClient
    {
        void ConnectToPipeServer(string pipeName, int timeout);
    }

    public class NamedPipeClient : INamedPipeClient
    {
        private const string ServerName = ".";
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void ConnectToPipeServer(string pipeName, int timeout)
        {
            using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(ServerName, pipeName, PipeDirection.Out))
            {
                try
                {
                    pipeClient.Connect(timeout);
                    Logger.Debug("Connection made to pipe: " + pipeName);
                }
                catch (TimeoutException) // No servers exist - do nothing
                {
                }
            }
        }
    }
}
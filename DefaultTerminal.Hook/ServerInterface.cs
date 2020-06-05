using System;

namespace DefaultTerminal.Hook
{
    public class ServerInterface : MarshalByRefObject
    {
        public static event PingRequestedHandler PingRequested;

        public delegate void PingRequestedHandler(string channelName);

        public static event ProcessCreatedEventHandler ProcessCreated;

        public delegate void ProcessCreatedEventHandler(string applicationName, string commandLine, int processId, bool isRedirected);

        public void RaisePingRequested(string channelName)
        {
            PingRequested?.Invoke(channelName);
        }

        public void RaiseProcessCreated(string applicationName, string commandLine, int processId, bool isRedirected)
        {
            ProcessCreated?.Invoke(applicationName, commandLine, processId, isRedirected);
        }

        public void WriteMessage(string value)
        {
            Console.WriteLine(value);
        }
    }
}

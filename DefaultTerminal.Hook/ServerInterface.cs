using System;
using System.Collections.Generic;

namespace DefaultTerminal.Hook
{
    public class ServerInterface : MarshalByRefObject
    {
        private readonly List<int> _createdPid = new List<int>();

        public static event ProcessCreatedEventHandler ProcessCreated;

        public delegate void ProcessCreatedEventHandler(string applicationName, string commandLine, int processId);

        public void OnProcessCreated(string applicationName, string commandLine, int processId)
        {
            if (_createdPid.Contains(processId))
                return;
            
            ProcessCreated?.Invoke(applicationName, commandLine, processId);
            _createdPid.Add(processId);
        }

        public void WriteMessage(string value)
        {
            Console.WriteLine(value);
        }
    }
}

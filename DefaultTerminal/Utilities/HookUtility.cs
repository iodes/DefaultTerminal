using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text.RegularExpressions;
using DefaultTerminal.Hook;
using EasyHook;

namespace DefaultTerminal.Utilities
{
    public static class HookUtility
    {
        #region Fields
        private static readonly string[] _recursiveHookTargets =
        {
            // Microsoft Visual Studio Remote Debugger
            "msvsmon.exe"
        };

        private static readonly Regex _commandLineRegex = new Regex("\"(.*?)\"");
        #endregion

        #region Constructor
        static HookUtility()
        {
            ServerInterface.PingRequested += ServerInterface_PingRequested;
            ServerInterface.ProcessCreated += ServerInterface_ProcessCreated;
        }
        #endregion

        #region Public Methdos
        public static void HookByProcessName(string processName)
        {
            foreach (var process in Process.GetProcessesByName(processName))
            {
                Hook(process.Id);
            }
        }

        public static void HookByProcessName(IEnumerable<string> processNames)
        {
            foreach (var name in processNames)
            {
                HookByProcessName(name);
            }
        }

        public static void Hook(int processId)
        {
            try
            {
                string channelName = null;
                RemoteHooking.IpcCreateServer<ServerInterface>(ref channelName, WellKnownObjectMode.Singleton);

                var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (string.IsNullOrEmpty(currentPath))
                    throw new InvalidOperationException();

                var injectionLibrary = Path.Combine(currentPath, "DefaultTerminal.Hook.dll");
                RemoteHooking.Inject(processId, injectionLibrary, injectionLibrary, channelName);

                Console.WriteLine($@"New Hook Success : {processId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Exception : {ex.Message}");
            }
        }
        #endregion

        #region Server Events
        private static void ServerInterface_PingRequested(string channelName)
        {
            Console.WriteLine($@"Ping : {channelName}");
        }

        private static void ServerInterface_ProcessCreated(string applicationName, string commandLine, int processId, bool isRedirected)
        {
            // Some programs require hooking recursively.
            var regexResult = _commandLineRegex.Match(commandLine);

            if (regexResult.Success)
            {
                var commandTarget = Path.GetFileName(regexResult.Groups[1].Value);

                if (!string.IsNullOrEmpty(commandTarget) && _recursiveHookTargets.Contains(commandTarget))
                    Hook(processId);
            }

            Console.WriteLine($@"Process Id : {processId}");
            Console.WriteLine($@"Process Command Line : {commandLine}");
            Console.WriteLine($@"Process Redirected : {isRedirected}");
        }
        #endregion
    }
}

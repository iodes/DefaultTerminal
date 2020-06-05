using System;
using System.Runtime.InteropServices;
using System.Threading;
using DefaultTerminal.Hook.Natives;
using DefaultTerminal.Hook.Natives.Structures;
using EasyHook;

namespace DefaultTerminal.Hook
{
    public class InjectionEntryPoint : IEntryPoint
    {
        #region Properties
        private ServerInterface Server { get; }
        #endregion

        #region Constructor
        public InjectionEntryPoint(RemoteHooking.IContext context, string channelName)
        {
            Server = RemoteHooking.IpcConnectClient<ServerInterface>(channelName);
        }
        #endregion

        #region Hook Methods
        private bool OnCreateProcessHook(string lpApplicationName, string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes, ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation)
        {
            bool isRedirected = false;
            var applicationName = lpApplicationName;
            var commandLine = lpCommandLine;

            // When dwCreationFlags is not CREATE_NO_WINDOW and lpApplicationName exists
            // Attempts to redirect the hooked request to Windows Terminal.
            if ((dwCreationFlags & 0x08000000) == 0 && !string.IsNullOrEmpty(lpApplicationName))
            {
                applicationName = null;
                commandLine = $"wt {lpCommandLine.Replace(";", "\\;")}";
                isRedirected = true;
            }

            var result = NativeMethods.CreateProcess(applicationName, commandLine, ref lpProcessAttributes,
                ref lpThreadAttributes, bInheritHandles, dwCreationFlags, lpEnvironment, lpCurrentDirectory,
                ref lpStartupInfo, out lpProcessInformation);

            Server.RaiseProcessCreated(lpApplicationName, lpCommandLine, lpProcessInformation.dwProcessId, isRedirected);

            return result;
        }
        #endregion

        #region Public Methods
        public void Run(RemoteHooking.IContext context, string channelName)
        {
            var createProcessHook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "CreateProcessW"), new NativeDelegates.CreateProcessDelegate(OnCreateProcessHook), this);
            createProcessHook.ThreadACL.SetExclusiveACL(new[] { 0 });

            RemoteHooking.WakeUpProcess();

            try
            {
                while (true)
                {
                    // Periodically send status to the server
                    Server.RaisePingRequested(channelName);
                    Thread.Sleep(1000);
                }
            }
            catch
            {
                // ignored
            }

            createProcessHook.Dispose();
            LocalHook.Release();
        }
        #endregion
    }
}

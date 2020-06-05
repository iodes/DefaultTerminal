using System;
using System.IO;
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

        #region Public Methods
        public void Run(RemoteHooking.IContext context, string channelName)
        {
            Server.WriteMessage("Installed : " + RemoteHooking.GetCurrentProcessId());

            var createProcessHook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "CreateProcessW"), new NativeDelegates.CreateProcessDelegate(CreateProcessHook), this);

            createProcessHook.ThreadACL.SetExclusiveACL(new[]
            {
                0
            });

            RemoteHooking.WakeUpProcess();

            try
            {
                while (true)
                {
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            createProcessHook.Dispose();
            LocalHook.Release();
        }
        #endregion

        #region Private Methods
        private string EscapeCommand(string value)
        {
            var escapeValue = value.Replace(";", "\\;");
            return escapeValue;
        }
        #endregion

        #region Hook Methods
        private bool CreateProcessHook(string lpApplicationName, string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes, ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation)
        {
            var applicationName = lpApplicationName;
            var commandLine = lpCommandLine;

            if ((dwCreationFlags & 0x08000000) == 0 && !string.IsNullOrEmpty(lpApplicationName))
            {
                applicationName = null;
                commandLine = $"wt {EscapeCommand(lpCommandLine)}";
                Server.WriteMessage($"Terminal is redirected : {lpCommandLine}");
            }

            var result = NativeMethods.CreateProcess(applicationName, commandLine, ref lpProcessAttributes,
                ref lpThreadAttributes, bInheritHandles, dwCreationFlags, lpEnvironment, lpCurrentDirectory,
                ref lpStartupInfo, out lpProcessInformation);

            Server.OnProcessCreated(lpApplicationName, lpCommandLine, lpProcessInformation.dwProcessId);

            return result;
        }
        #endregion
    }
}

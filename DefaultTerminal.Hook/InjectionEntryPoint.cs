using System;
using System.Diagnostics;
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

        #region Hook Methods
        private bool OnCreateProcessHook(string lpApplicationName, string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes, ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation)
        {
            bool isRedirected = false;

            // Only when dwCreationFlags is not CREATE_NO_WINDOW
            if ((dwCreationFlags & 0x08000000) == 0)
            {
                var currentProcess = Process.GetProcessById(RemoteHooking.GetCurrentProcessId());

                if (currentProcess.ProcessName == "explorer")
                {
                    // If Windows Explorer, redirect only in some cases
                    switch (Path.GetFileName(lpApplicationName))
                    {
                        case "powershell.exe":
                        case "cmd.exe":
                        case null:
                            isRedirected = Redirect(ref lpApplicationName, ref lpCommandLine);
                            break;
                    }
                }
                else if (!string.IsNullOrEmpty(lpApplicationName))
                {
                    // In other cases, redirect only when lpApplicationName exists
                    isRedirected = Redirect(ref lpApplicationName, ref lpCommandLine);
                }
            }

            var result = NativeMethods.CreateProcess(lpApplicationName, lpCommandLine, ref lpProcessAttributes, ref lpThreadAttributes,
                bInheritHandles, dwCreationFlags, lpEnvironment, lpCurrentDirectory, ref lpStartupInfo, out lpProcessInformation);

            Server.RaiseProcessCreated(lpApplicationName, lpCommandLine, lpProcessInformation.dwProcessId, isRedirected);

            return result;
        }
        #endregion

        #region Private Methods
        private bool Redirect(ref string lpApplicationName, ref string lpCommandLine)
        {
            if (!string.IsNullOrEmpty(lpApplicationName))
                lpApplicationName = null;

            lpCommandLine = $"wt {lpCommandLine.Replace(";", "\\;")}";

            return true;
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

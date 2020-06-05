using System;
using System.Runtime.InteropServices;
using DefaultTerminal.Hook.Natives.Structures;

namespace DefaultTerminal.Hook.Natives
{
    internal static class NativeDelegates
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        internal delegate bool CreateProcessDelegate([MarshalAs(UnmanagedType.LPWStr)] string lpApplicationName, [MarshalAs(UnmanagedType.LPWStr)] string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes, ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, [MarshalAs(UnmanagedType.LPWStr)] string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);
    }
}

using System;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Windows;
using DefaultTerminal.Hook;
using EasyHook;

namespace DefaultTerminal
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Hook(18328);
            ServerInterface.ProcessCreated += ServerInterface_ProcessCreated;
        }

        private void Hook(int processId)
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
            }
            catch
            {

            }
        }

        private void ServerInterface_ProcessCreated(string applicationName, string commandLine, int processId)
        {
            Console.WriteLine($"Process ID : {processId}");
            Console.WriteLine($"Application Name : {applicationName}");
            Console.WriteLine($"Command Line : {commandLine}");

            if (processId > 0 && commandLine.Contains("msvsmon.exe"))
            {
                Hook(processId);
                Console.WriteLine("Hook created!");
            }
        }
    }
}

using System.Diagnostics;
using System.Windows;

namespace ServerLauncher.Server
{

    class CommandExecutor
    {
        public static void Command(string command)
        {
            Process server = ((MainWindow)Application.Current.MainWindow).JavaServer.ServerProcess;
            if(server != null)
                server.StandardInput.WriteLine(command);
        }
        public static void Command(Process serverInstance, string command)
        {
            if (serverInstance != null)
                serverInstance.StandardInput.WriteLine(command);
        }


    }
}

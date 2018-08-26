using System.Diagnostics;
using System.Windows;

namespace ServerLauncher.Server
{
    public class CommandExecutor
    {
        public static void Command(string command)
        {
            var server = ((MainWindow)Application.Current.MainWindow).JavaServer.ServerProcess;
            server?.StandardInput.WriteLine(command);
        }
        public static void Command(Process serverInstance, string command)
        {
            serverInstance?.StandardInput.WriteLine(command);
        }
    }
}

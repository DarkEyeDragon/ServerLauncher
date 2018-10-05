using System.Diagnostics;
using System.Windows;
using ServerLauncher.Config;

namespace ServerLauncher.Server
{
    public class CommandExecutor
    {
        public static void Command(string command)
        {
            var server = ((MainWindow)Application.Current.MainWindow).JavaServer.ServerProcess;
            if (command.StartsWith("-"))
            {
                command = command.TrimStart('-');
                if (command.Equals("reset"))
                {
                    ConfigHandler.SetDefault();

                }else if (command.Equals("exit") || command.Equals("stop"))
                {
                    Application.Current.Shutdown();

                }
            }
            else
            {
                server?.StandardInput.WriteLine(command);
            }
        }
        public static void Command(Process serverInstance, string command)
        {
            var server = ((MainWindow)Application.Current.MainWindow).JavaServer.ServerProcess;
            if (command.StartsWith("-"))
            {
                command = command.TrimStart('-');
                if (command.Equals("reset"))
                {
                    ConfigHandler.SetDefault();

                }
                else if (command.Equals("exit") || command.Equals("stop"))
                {
                    Application.Current.Shutdown();
                }
            }
            else
            {
                server?.StandardInput.WriteLine(command);
            }
        }
    }
}

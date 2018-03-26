using System.Diagnostics;

namespace ServerLauncher.Server
{
    class InputHandler
    {
        public static void Command(Process serverInstance, string command)
        {
            serverInstance.StandardInput.WriteLine(command);
        }
    }
}

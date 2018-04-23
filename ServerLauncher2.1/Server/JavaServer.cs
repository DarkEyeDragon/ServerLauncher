using ServerLauncher.Client;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace ServerLauncher.Server
{
    class JavaServer : IServer
    {

        private string ServerJar { get; set; }
        private Process process;
        public MainWindow MainWin { get; set; }
        private string Folder { get; set; }
        private string XMS { get; set; }

        public string XMX { get; set; }

        public Process ServerProcess { get => process; }

        public JavaServer(string xms, string xmx)
        {
            XMS = xms;
            XMX = xmx;
            MainWin = ((MainWindow)(Application.Current.MainWindow));
            Folder = MainWin.inputJarLocation.Text;
            ServerJar = Path.GetFileName(MainWin.inputJarLocation.Text);
        }

        public void Start()
        {
            Output($"Starting server with XMS: {XMS} and XMX: {XMX}");
            process = new Process();
            process.StartInfo.FileName = "java";
            process.StartInfo.Arguments = $"-Xms{XMS} -Xmx{XMX} -jar {ServerJar} nogui";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(Folder);
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = false;
            process.OutputDataReceived += (sender, args) => { Output(args.Data); };
            process.Start();
            process.BeginOutputReadLine();
        }

        public void Stop()
        {
            process.StandardInput.WriteLine("stop");

        }

        public void Output(String output)
        {
            if (string.IsNullOrEmpty(output))
                return;
            
            if (output.Contains("WARN]: "))
                OutputHandler.Log(output, Level.WARN);
            else if (output.Contains("ERROR]: "))
                OutputHandler.Log(output, Level.ERROR);
            else if (output.Contains("INFO]: UUID of player "))
            {
                Player joinedPlayer = new Player();
                string[] splitted = output.Split(' ');
                joinedPlayer.Username = splitted[5];
                joinedPlayer.UUID = splitted[7];
                PlayerListManager.PlayerList.Add(joinedPlayer);
                PlayerListManager.Display(MainWin);

            }
            if (output.Contains("lost connection: "))
            {
                string[] disconnectMessage = output.Split(' ');

                PlayerListManager.Remove(disconnectMessage[2], MainWin);
                PlayerListManager.Display(MainWin);
                OutputHandler.Log(output);
                
            }


            else
                OutputHandler.Log(output);
                
        }
        public void Input(String input)
        {
            InputHandler.Command(process, input);
        }
    }
}

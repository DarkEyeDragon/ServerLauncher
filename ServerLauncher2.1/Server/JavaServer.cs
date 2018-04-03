using ServerLauncher.Client;
using System;
using System.Diagnostics;
using System.IO;

namespace ServerLauncher.Server
{
    class JavaServer : IServer
    {

        private string xms;
        private string ServerJar { get; set; }
        private Process process;
        public MainWindow MainWin { get; set; }
        private string Folder { get; set; }
        private string XMS
        {
            get { return xms; }
            set { xms = value; }
        }
        private string xmx;

        public string XMX
        {
            get { return xmx; }
            set { xmx = value; }
        }

        public Process ServerProcess { get => process; }

        public JavaServer(string xms, string xmx, string jarLocation, MainWindow mainwindow)
        {
            XMS = xms;
            XMX = xmx;
            ServerJar = jarLocation.Split('/')[1];
            Folder = jarLocation.Split('/')[0];
            MainWin = mainwindow;
        }

        public void Start()
        {
            Output($"Starting server with XMS: {XMS} and XMX: {XMX}");
            process = new Process();
            process.StartInfo.FileName = "java";
            process.StartInfo.Arguments = $"-Xms{XMS} -Xmx{XMX} -jar {ServerJar} nogui";
            process.StartInfo.UseShellExecute = false;
            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, Folder));
            process.StartInfo.WorkingDirectory = Path.Combine(Environment.CurrentDirectory, Folder);
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
                OutputHandler.Log(output, Level.WARN, MainWin);
            else if (output.Contains("ERROR]: "))
                OutputHandler.Log(output, Level.ERROR, MainWin);
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
                OutputHandler.Log(output, MainWin);
                
            }


            else
                OutputHandler.Log(output, MainWin);
                
        }
        public void Input(String input)
        {
            InputHandler.Command(process, input);
        }
    }
}

using ServerLauncher.Client;
using ServerLauncher.Graphs;
using ServerLauncher.Timers;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace ServerLauncher.Server
{
    public class JavaServer : IServer
    {

        private string ServerJar { get; set; }
        private Process process;
        public MainWindow MainWin { get; set; }
        private string Folder { get; set; }
        private string XMS { get; set; }
        private int currentRam;

        PointShapeLine graph;
        RamChartUpdater updater;


        public int RAM
        {
            get {
                process.Refresh();
                if (!process.HasExited)
                    return (int)process.PrivateMemorySize64/ 1048576;
                else
                    return 0;
            }
            set { currentRam = value; }
        }


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
            //TODO FIX GRAPH
            if (graph == null)
            {
                graph = new PointShapeLine();
            }
            if (updater == null)
                updater = new RamChartUpdater(this, graph);
            else
                updater.Timer.Start();
            Watchdog.Start();
        }

        public void Stop()
        {
            process.StandardInput.WriteLine("stop");
            graph.Clear();
            updater.Timer.Stop();
            Watchdog.Stop();
        }

        public void Output(String output)
        {
            Watchdog.running = true;
            if (string.IsNullOrEmpty(output))
                return;

            if (output.Contains("WARN]: "))
                OutputHandler.Log(output, Level.WARN);
            else if (output.Contains("ERROR]: "))
                OutputHandler.Log(output, Level.ERROR);
            else if (output.Contains("INFO]: "))
            {
                if (output.Contains("INFO]: UUID of player "))
                {
                    Player joinedPlayer = new Player();
                    string[] splitted = output.Split(' ');
                    joinedPlayer.Username = splitted[5];
                    joinedPlayer.UUID = splitted[7];
                    PlayerListManager.Add(joinedPlayer, MainWin);

                }
                OutputHandler.Log(output);
            }
            else
                OutputHandler.Log(output);
            if (output.Contains("lost connection: "))
            {
                string[] disconnectMessage = output.Split(' ');

                PlayerListManager.Remove(disconnectMessage[2], MainWin);
                OutputHandler.Log(output);
            }
            else if (output.ToLower().Contains("**** failed to bind to port"))
            {
                process.StandardInput.WriteLine("stop");
                updater.Timer.Stop();
                Watchdog.Stop();
            }
                
        }
        public void Input(String input)
        {
            CommandExecutor.Command(process, input);
        }
    }
}

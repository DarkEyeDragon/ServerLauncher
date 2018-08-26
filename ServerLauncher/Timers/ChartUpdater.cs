using ServerLauncher.Graphs;
using ServerLauncher.Server;
using System;
using System.Windows;
using System.Windows.Threading;
using ServerLauncher.Client;

namespace ServerLauncher.Timers
{
    class RamChartUpdater
    {
        private JavaServer Server { get; }
        private LineGraph Graph { get; }
        public DispatcherTimer Timer { get; set; }
        public DataSet DataSet { get; set; }

        public RamChartUpdater(LineGraph graph, DataSet dataSet, JavaServer server)
        {
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            Timer.Tick += timer_Tick;
            Timer.Start();
            Graph = graph;
            DataSet = dataSet;
            Server = server;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            
            Application.Current.Dispatcher.Invoke(() =>
            {

                Graph.Clear();
                Graph.DataSet = DataSet;
                double megabytesUsed = Server.GetBytesUsed() / 1048576;
                DataSet.Insert(Server.GetBytesUsed()/ 1048576);
                if (megabytesUsed < Server.XMXInt)
                {
                    Graph.Max = Server.XMXInt;
                }
                else
                {
                    Graph.Max = (int)Math.Ceiling(megabytesUsed);
                }
                Graph.ScaleY = Graph.Height / Graph.Max;
                Graph.ScaleX = Graph.Width / DataSet.Size;
                Graph.Draw();
                ((MainWindow)Application.Current.MainWindow).RamUsagePercent = $"{(int)Math.Round(megabytesUsed/Graph.Max*100)}%";
                ((MainWindow)Application.Current.MainWindow).RamUsageMegabyte = $"{((int)Math.Round(megabytesUsed))}MB";
            });
        }
    }
}

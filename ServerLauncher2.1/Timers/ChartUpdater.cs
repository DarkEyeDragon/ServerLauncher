using ServerLauncher.Graphs;
using ServerLauncher.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ServerLauncher.Client;

namespace ServerLauncher.Timers
{
    class RamChartUpdater
    {
        private JavaServer Server { get; set; }
        private LineGraph Graph { get; set; }
        public DispatcherTimer Timer { get; set; }
        public DataSet DataSet { get; set; }
        private Random random = new Random();
        public RamChartUpdater(LineGraph graph, DataSet dataSet, JavaServer server)
        {
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
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
                DataSet.Insert(Server.RAM);
                Graph.Draw();
            });
            /*if (Graph.SeriesCollection[0].Values.Count == 10)
            {
                Graph.SeriesCollection[0].Values.RemoveAt(0);
                Graph.SeriesCollection[0].Values.Add(Convert.ToInt32(Server.RAM));
            }
            else
            {
                Graph.SeriesCollection[0].Values.Add(Convert.ToInt32(Server.RAM));

            }*/


        }
    }
}

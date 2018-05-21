using ServerLauncher.Graphs;
using ServerLauncher.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ServerLauncher.Timers
{
    class RamChartUpdater
    {
        private JavaServer Server { get; set; }
        private PointShapeLine Graph { get; set; }
        public DispatcherTimer Timer { get; set; }
        public RamChartUpdater(JavaServer server, PointShapeLine graph)
        {
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            Timer.Tick += timer_Tick;
            Timer.Start();
            Server = server;
            Graph = graph;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (Graph.SeriesCollection[0].Values.Count == 10)
            {
                Graph.SeriesCollection[0].Values.RemoveAt(0);
                Graph.SeriesCollection[0].Values.Add(Convert.ToInt32(Server.RAM));
            }
            else
            {
                Graph.SeriesCollection[0].Values.Add(Convert.ToInt32(Server.RAM));

            }


        }
    }
}

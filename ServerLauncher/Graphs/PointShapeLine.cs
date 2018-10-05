using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ServerLauncher.Graphs
{
    /*public class PointShapeLine : UserControl
    {
        MainWindow main;
        public PointShapeLine()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Ram(MB)",
                    Values = new ChartValues<int> {}
                }
            };
            main = ((MainWindow)(Application.Current.MainWindow));
            main.stats.Series.Add(SeriesCollection[0]);
        }

        public void AddData(int datapoint)
        {
            SeriesCollection[0].Values.Add(datapoint);
        }
        public SeriesCollection SeriesCollection { get; }

        public void Clear()
        {
            while (main.stats.Series.Count > 0) { main.stats.Series.RemoveAt(0); }
        }
    }*/
}

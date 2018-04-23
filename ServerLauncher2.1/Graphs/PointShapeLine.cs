using LiveCharts;
using LiveCharts.Wpf;
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
    public class PointShapeLine : UserControl
    {

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
            MainWindow main = ((MainWindow)(Application.Current.MainWindow));
            main.stats.Series.Add(SeriesCollection[0]);
        }

        public void AddData(int datapoint)
        {
            SeriesCollection[0].Values.Add(datapoint);
        }
        public SeriesCollection SeriesCollection { get; }
    }
}

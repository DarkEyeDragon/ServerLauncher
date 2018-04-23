using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ServerLauncher.Graphs
{
    public class PointShapeLine : UserControl
    {
        private long data;

        public PointShapeLine(long dataPoint)
        {
            data = dataPoint;
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<long> { 0 }
                },
            };

            Labels = new[] { "0", "60"};
            YFormatter = value => value.ToString("I DONT KONW");

            //modifying the series collection will animate and update the chart
            SeriesCollection.Add(new LineSeries
            {
                Title = "Something",
                Values = new ChartValues<long> { data },
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                PointGeometrySize = 50,
                PointForeground = Brushes.Gray
            });

            //modifying any series values will also animate and update the chart
            SeriesCollection[0].Values.Add(data);
            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

    }
}

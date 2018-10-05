using ServerLauncher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using ServerLauncher.Client;

namespace ServerLauncher.Graphs
{
    class LineGraph
    {
        private readonly MainWindow main;
        public DataSet DataSet { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int Max { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public LineGraph(DataSet dataSet, int max)
        {
            main = ((MainWindow)(Application.Current.MainWindow));
            if (main != null)
            {
                Width = main.lineGraph.ActualWidth+1;
                Height = main.lineGraph.ActualHeight+1;
                main.lineGraph.Children.Clear();
                Max = max;
            }
            DataSet = dataSet;
        }
        //TODO FIX GRAPH
        public void Draw()
        {
            Width = main.lineGraph.ActualWidth;
            Height = main.lineGraph.ActualHeight;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Colors.CornflowerBlue;
            for (int x = 0; x < DataSet.DataDoubleList.Count; x++)
            {

                if (x + 1 < DataSet.DataDoubleList.Count)
                {
                    var dataLine = new Line
                    {
                        X1 = x * ScaleX,
                        Y1 = Height - (DataSet.DataDoubleList[x]) * ScaleY,
                        Stroke = color,
                        X2 = (x + 1) * ScaleX,
                        Y2 = Height - (DataSet.DataDoubleList[x+1]) * ScaleY,
                        StrokeThickness = 1
                    };
                    //Debug.WriteLine($"Y1:{dataLine.Y1} Y2:{dataLine.Y2} DataSet:{DataSet.DataDoubleList[x]}");
                    main.lineGraph.Children.Add(dataLine);
                }
                else
                {
                    var dataLine = new Line
                    {
                        X1 = x * ScaleX,
                        Y1 = Height - (DataSet.DataDoubleList[x])* ScaleY,
                        Stroke = color,
                        X2 = (x) * ScaleX,
                        Y2 = Height - (DataSet.DataDoubleList[x])* ScaleY,
                        StrokeThickness = 1

                    };
                    //Debug.WriteLine($"Y1:{dataLine.Y1} Y2:{dataLine.Y2} DataSet:{DataSet.DataDoubleList[x]}");
                    main.lineGraph.Children.Add(dataLine);
                }
            }
        }

        public void Clear()
        {
            main.lineGraph.Children.Clear();
        }
    }
}

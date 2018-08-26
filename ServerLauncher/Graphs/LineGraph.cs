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

            double scaleX = Width / DataSet.Size;
            double scaleY = Height / DataSet.Max();
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Colors.OrangeRed;
            for (int x = 0; x < DataSet.DataIntsList.Count; x++)
            {

                if (x + 1 < DataSet.DataIntsList.Count)
                {
                    Line dataLine = new Line
                    {
                        X1 = x * scaleX,
                        Y1 = Height - (DataSet.DataIntsList[x]) * scaleY,
                        Stroke = color,
                        X2 = (x + 1) * scaleX,
                        Y2 = Height - (DataSet.DataIntsList[x+1]) * scaleY,
                        StrokeThickness = 1
                    };
                    Debug.WriteLine($"Y1:{dataLine.Y1} Y2:{dataLine.Y2} DataSet:{DataSet.DataIntsList[x]}");
                    main.lineGraph.Children.Add(dataLine);
                }
                else
                {
                    Line dataLine = new Line
                    {
                        X1 = x * scaleX,
                        Y1 = Height - (DataSet.DataIntsList[x])*scaleY,
                        Stroke = color,
                        X2 = (x) * scaleX,
                        Y2 = Height - (DataSet.DataIntsList[x])*scaleY,
                        StrokeThickness = 1

                    };
                    Debug.WriteLine($"Y1:{dataLine.Y1} Y2:{dataLine.Y2} DataSet:{DataSet.DataIntsList[x]}");
                    main.lineGraph.Children.Add(dataLine);
                }

                /*if (x + 1 <= DataSet.Size)
                {
                    Line dataLine = new Line
                    {
                        X1 = x * scaleX,
                        Y1 = DataSet.DataIntsList[x],
                        Stroke = color,
                        X2 = (x+1) * scaleX,
                        Y2 = DataSet.DataIntsList[x],
                        StrokeThickness = 1

                    };
                    Debug.WriteLine($"Y1:{dataLine.Y1} Y2:{dataLine.Y2} DataSet:{DataSet.DataIntsList[x ]}");
                    main.lineGraph.Children.Add(dataLine);
                }*/

                if (x + 1 < DataSet.Size)
                {
                    
                }
            }
        }

        public void Clear()
        {
            main.lineGraph.Children.Clear();
        }
    }
}

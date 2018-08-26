using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace ServerLauncher.Graphs
{
    class GridBase
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public GridBase()
        {
           
        }
        public GridBase(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public void Draw()
        {
            for (int y = 0; y < 9; y++)
            {
                var horizontalLine = new Line
                {
                    //TODO add grid
                };
            }
            
        }
    }
}

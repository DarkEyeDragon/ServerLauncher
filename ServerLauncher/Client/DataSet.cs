using System.Collections.Generic;
using System.Linq;

namespace ServerLauncher.Client
{
    class DataSet : List<double>
    {

        public List<double> DataDoubleList { get; set; }

        public int Size { get; set; }
        public DataSet(int size)
        {
            Size = size;
            DataDoubleList = new List<double>(size);
        }

        public void Insert(double value)
        {
            if (DataDoubleList.Count < Size)
            {
                DataDoubleList.Add(value);
            }
            else
            {
                double[] dataIntArray = DataDoubleList.ToArray();
                DataDoubleList = dataIntArray.Skip(1).Concat(new []{value}).ToList();
            }
        }

        public double Max()
        {
            return DataDoubleList.ToArray().Max();
        }

        public double Get(int index)
        {
            return DataDoubleList.IndexOf(index);
        }

        public List<double> ToList()
        {
            return DataDoubleList;
        }
    }
}

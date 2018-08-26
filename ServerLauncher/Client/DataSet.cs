using System.Collections.Generic;
using System.Linq;

namespace ServerLauncher.Client
{
    class DataSet
    {

        public List<double> DataIntsList { get; set; }

        public int Size { get; set; }
        public DataSet(int size)
        {
            Size = size;
            DataIntsList = new List<double>(size);
        }

        public void Insert(double value)
        {
            if (DataIntsList.Count < Size)
            {
                DataIntsList.Add(value);
            }
            else
            {
                double[] dataIntArray = DataIntsList.ToArray();
                DataIntsList = dataIntArray.Skip(1).Concat(dataIntArray.Take(1)).ToList();
            }
        }

        public double Max()
        {
            return DataIntsList.ToArray().Max();
        }

        public double Get(int index)
        {
            return DataIntsList.IndexOf(index);
        }

        public double[] ToArray()
        {
            return DataIntsList.ToArray();
        }

        public List<double> ToList()
        {
            return DataIntsList;
        }
    }
}

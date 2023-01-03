using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartesianCore
{
    public class Dataset
    {
        internal List<Data> DatasetList;
        public int DatasetID { get; private set; }
        public Dataset(int datasetID)
        {
            DatasetList = new List<Data>();
            xMin = double.MaxValue;
            yMin = double.MaxValue;
            xMax = double.MinValue;
            yMax = double.MinValue;
            DatasetID = datasetID; //To identify dataset while drawing for coloring, line thickness etc..
        }
        public double xMin;
        public double yMin;
        public double xMax;
        public double yMax;

        public void Add(double xValue, double yValue)
        {
            CalculateMinMax(xValue, yValue);
            DatasetList.Add(new Data(xValue, yValue));
        }

        private void CalculateMinMax(double x, double y)
        {
            if (x < xMin)
                xMin = x;

            if (x > xMax)
                xMax = x;

            if (y < yMin)
                yMin = y;

            if (y > yMax)
                yMax = y;
        }
    }

    internal struct Data
    {
        public Data(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        public double X;
        public double Y;
    }
}

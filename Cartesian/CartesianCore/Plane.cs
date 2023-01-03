using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartesianCore
{
    public class Plane
    {
        public Plane(int width, int height, int topMargin = 0, int bottomMargin = 0, int leftMargin = 0, int rightMargin = 0)
        {
            this.width = width - (leftMargin + rightMargin);
            this.height = height - (topMargin + bottomMargin);
            this.topMargin = topMargin;
            this.bottomMargin = bottomMargin;
            this.leftMargin = leftMargin;
            this.rightMargin = rightMargin;

            XAxisMin = double.MaxValue;
            YAxisMin = double.MaxValue;
            XAxisMax = double.MinValue;
            YAxisMax = double.MinValue;

            DatasetList = new Dictionary<int, Dataset>();

            planeXStart = leftMargin;
            planeYStart = topMargin;

        }
        public Dictionary<int, Dataset> DatasetList;
        public List<Point> Datapoints;

        readonly int width, height, topMargin = 0, bottomMargin = 0, rightMargin = 0, leftMargin = 0;
        double xUnitLength, yUnitLength;
        private bool axesSet = false;
        readonly int planeXStart, planeYStart;

        public double XAxisMin { get; private set; }
        public double XAxisMax { get; private set; }

        public double YAxisMin { get; private set; }
        public double YAxisMax { get; private set; }

        public void AddDataset(Dataset dataset)
        {
            if (dataset != null)
                DatasetList.Add(dataset.DatasetID, dataset);

            if (!axesSet)
                CalculateMinMax(dataset);

            CalculateUnitLengths();
        }

        private void CalculateMinMax(Dataset dataset)
        {
            if (dataset.xMin < XAxisMin)
                XAxisMin = dataset.xMin;

            if (dataset.xMax > XAxisMax)
                XAxisMax = dataset.xMax;

            if (dataset.yMin < YAxisMin)
                YAxisMin = dataset.yMin;

            if (dataset.yMax > YAxisMax)
                YAxisMax = dataset.yMax;
        }

        private void CalculateUnitLengths()
        {
            if (XAxisMax - XAxisMin > 0)
                xUnitLength = width / (XAxisMax - XAxisMin);
            else
                throw new Exception($"Difference between X axis maximum ({XAxisMax}) and minimum ({XAxisMin}) must be greater than 0");

            if (YAxisMax - YAxisMin > 0)
                yUnitLength = height / (YAxisMax - YAxisMin);
            else
                throw new Exception($"Difference between Y axis maximum ({YAxisMax}) and minimum ({YAxisMin}) must be greater than 0");
        }

        public void SetAxes(double xMin, double xMax, double yMin, double yMax)
        {
            axesSet = true;
            this.XAxisMin = xMin;
            this.XAxisMax = xMax;
            this.YAxisMin = yMin;
            this.YAxisMax = yMax;

            CalculateUnitLengths();
        }

        public Point[] GetDataset(int datasetID)
        {
            Dataset dataset = DatasetList[datasetID];
            Point[] points = new Point[dataset.DatasetList.Count];
            for (int i = 0; i < points.Length; i++)
            {
                points[i].X = (dataset.DatasetList[i].X - XAxisMin) * xUnitLength + planeXStart;
                points[i].Y = height - ((dataset.DatasetList[i].Y - YAxisMin) * yUnitLength) + planeYStart;
            }

            return points;
        }

        public Point[] GetHorizontalLine(double y)
        {
            Point[] points = new Point[2]; //we need start and end
            points[0].X = planeXStart;
            points[0].Y = height - (y - YAxisMin) * yUnitLength + planeYStart;

            points[1].X = planeXStart + width;
            points[1].Y = points[0].Y;

            return points;
        }

        public Point[] GetVerticalLine(double x)
        {
            Point[] points = new Point[2]; //we need start and end
            points[0].X = (x - XAxisMin) * xUnitLength + planeXStart;
            points[0].Y = planeYStart;

            points[1].X = points[0].X;
            points[1].Y = planeYStart + height;

            return points;
        }

        public Point GetPoint(double x, double y)
        {
            Point point = new Point
            {
                X = (x - XAxisMin) * xUnitLength + planeXStart,
                Y = height - ((y - YAxisMin) * yUnitLength) + planeYStart
            };

            return point;
        }
    }

    public struct Point
    {
        public double X;
        public double Y;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartesianCore;
using System.Drawing;
using System.Drawing.Imaging;

namespace CartesianBitmapDrawer
{
    class Program
    {
        static void Main(string[] args)
        {

            #region GenerateSomeData
            var r = new Random();
            int sample = 1200;
            int timeStart = -600;
            float coef = 0.005f;
            float[] timeData = new float[sample];
            float[] sinData = new float[sample];
            float[] someData = new float[sample];

            for (int i = 0; i < sample; i++)
            {
                timeData[i] = (i + timeStart) * coef;
            }

            for (int i = 0; i < sample; i++)
            {
                var t = (i + timeStart) * coef;
                sinData[i] = (float)Math.Sin(4 * t);
                someData[i] = t * (float)Math.Sin(Math.Pow(t, 2));//sinData[i] + (float)Math.Cos(i * 0.01f) + (float)(r.NextDouble() -0.5) * 0.1f;
            }
            #endregion

            int height = 1080;
            int width = 1920;

            Bitmap b = new System.Drawing.Bitmap(width, height);
            Graphics g = Graphics.FromImage(b);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillRegion(new SolidBrush(Color.FromArgb(30, 30, 30)), new Region(new Rectangle(0, 0, width, height)));


            var plane = new Plane(width, height, 50, 50, 50, 50);

            Dataset dataset_A = new Dataset(1);
            Dataset dataset_B = new Dataset(2);

            for (int i = 0; i < sample; i++)
            {
                dataset_A.Add(timeData[i], someData[i]);
                dataset_B.Add(timeData[i], sinData[i]);
            }


            plane.AddDataset(dataset_A);
            plane.AddDataset(dataset_B);

            plane.SetAxes(MathTools.Floor(plane.XAxisMin), MathTools.Ceil(plane.XAxisMax), MathTools.Floor(plane.YAxisMin), MathTools.Ceil(plane.YAxisMax));


            CartesianCore.Point[] points;

            var color = Color.FromArgb(51, 51, 51);

            for (int i = (int)plane.XAxisMin; i <= plane.XAxisMax; i++)
            {
                points = plane.GetVerticalLine(i);
                DrawLines(g, points, color, 1);
                //lets write the value
                DrawString(g, $"{i}", plane.GetPoint(i, 0), Color.LightGray, new Font("consolas", 14));
            }

            for (int i = (int)plane.YAxisMin; i <= plane.YAxisMax; i++)
            {
                points = plane.GetHorizontalLine(i);
                DrawLines(g, points, color, 1);
                //lets write the value
                DrawString(g, $"{i}", plane.GetPoint(0, i), Color.LightGray, new Font("consolas", 14));
            }

            points = plane.GetHorizontalLine(0);
            DrawLines(g, points, Color.White, 1);

            points = plane.GetVerticalLine(0);
            DrawLines(g, points, Color.White, 1);

            points = plane.GetDataset(1);
            DrawPoints(g, points, Color.Blue, 3);

            points = plane.GetDataset(2);
            DrawPoints(g, points, Color.Green, 3);

            b.Save("result.png", ImageFormat.Png);

        }

        static void DrawLines(Graphics g, CartesianCore.Point[] points, Color color, float tickness)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                g.DrawLine(new Pen(color, tickness), new PointF((float)points[i].X, (float)points[i].Y), new PointF((float)points[i + 1].X, (float)points[i + 1].Y));
            }
        }

        static void DrawPoints(Graphics g, CartesianCore.Point[] points, Color color, float tickness)
        {
            for (int i = 0; i < points.Length; i++)
            {
                g.DrawEllipse(new Pen(color, tickness), (float)points[i].X, (float)points[i].Y, 2, 2);
            }
        }

        static void DrawString(Graphics g, string value, CartesianCore.Point point, Color color, Font font)
        {
            g.DrawString(value, font, new SolidBrush(color), (float)point.X, (float)point.Y);
        }
    }
}

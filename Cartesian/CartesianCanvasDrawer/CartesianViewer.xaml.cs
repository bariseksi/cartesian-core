using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CartesianCore;
using System.Threading;

namespace CartesianCanvasDrawer
{

    public class TextShape : FrameworkElement
    {
        public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(float), typeof(TextShape), new FrameworkPropertyMetadata(0.0f, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(float), typeof(TextShape), new FrameworkPropertyMetadata(0.0f, FrameworkPropertyMetadataOptions.AffectsRender));

        public string Text { get; set; }
        public float X
        {
            get { return (float)this.GetValue(XProperty); }
            set { this.SetValue(XProperty, value); }
        }

        public float Y
        {
            get { return (float)this.GetValue(YProperty); }
            set { this.SetValue(YProperty, value); }
        }

        public System.Windows.Media.Color TextColor { get; set; }
        public float TextHeight { get; set; }

        readonly FormattedText ft;
        System.Windows.Point p;

        public TextShape(string text, Typeface typeface, double height, Color color, float x, float y)
        {
            ft = new FormattedText(text, new CultureInfo("en-us"), FlowDirection.LeftToRight, typeface, height, new SolidColorBrush(color));

            X = x;
            Y = y;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            p.X = X;
            p.Y = Y;
            drawingContext.DrawText(ft, p);
        }
    }


    /// <summary>
    /// Interaction logic for CartesianViewer.xaml
    /// </summary>
    public partial class CartesianViewer : UserControl
    {

        float[] timeData = null;
        float[] sinData = null;
        float[] someData = null;
        Typeface fontFace = null;

        
        public CartesianViewer()
        {
            InitializeComponent();

            #region GenerateSomeData
            var r = new Random();
            int resolution = 1;
            int sample = 1200 / resolution;
            int timeStart = -600 / resolution;
            float coef = 0.005f * resolution;
            timeData = new float[sample];
            sinData = new float[sample];
            someData = new float[sample];

            for (int i = 0; i < sample; i++)
            {
                timeData[i] = (i + timeStart) * coef;
            }

            for (int i = 0; i < sample; i++)
            {
                var t = (i + timeStart) * coef;
                sinData[i] = (float)Math.Cos(4*t);
                someData[i] = t * (float)Math.Sin(Math.Pow(t, 2));
            }
            #endregion

            fontFace = new Typeface(new FontFamily("consolas"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

            int width = 1800;
            int height = 1000;
            CanvasObj.Background = new SolidColorBrush(Color.FromArgb(255, 30, 30, 30));
            CanvasObj.Height = height;
            CanvasObj.Width = width;

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

            var color = Color.FromRgb(51, 51, 51);

            CartesianCore.Point[] points;

            for (int i = (int)plane.XAxisMin; i <= plane.XAxisMax; i++)
            {
                points = plane.GetVerticalLine(i);
                DrawLines(CanvasObj, points, color, 1);
                //lets write the value
                DrawString(CanvasObj, $"{i}", plane.GetPoint(i, 0), 14, Colors.LightGray, new Typeface("consolas"));
            }

            for (int i = (int)plane.YAxisMin; i <= plane.YAxisMax; i++)
            {
                points = plane.GetHorizontalLine(i);
                DrawLines(CanvasObj, points, color, 1);
                //lets write the value
                DrawString(CanvasObj, $"{i}", plane.GetPoint(0, i), 14, Colors.LightGray, new Typeface("consolas"));
            }

            points = plane.GetHorizontalLine(0);
            DrawLines(CanvasObj, points, Colors.White, 1);

            points = plane.GetVerticalLine(0);
            DrawLines(CanvasObj, points, Colors.White, 1);

            points = plane.GetDataset(1);
            DrawLines(CanvasObj, points, Colors.Blue, 3);

            points = plane.GetDataset(2);
            DrawLines(CanvasObj, points, Colors.Green, 3);
        }

        public int DrawLines(Canvas canvas, CartesianCore.Point[] points, Color color, float thickness)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                var line = new Line()
                {
                    X1 = points[i].X,
                    Y1 = points[i].Y,
                    X2 = points[i + 1].X,
                    Y2 = points[i + 1].Y,
                    Stroke = new SolidColorBrush(color),
                    StrokeThickness = thickness,
                    StrokeEndLineCap = PenLineCap.Round

                };
                canvas.Children.Add(line);
            }
            return canvas.Children.Count;
        }

        public int DrawPoints(Canvas canvas, CartesianCore.Point[] points, Color color, float thickness)
        {
            for (int i = 0; i < points.Length; i++)
            {
                var point = new System.Windows.Shapes.Ellipse()
                {
                    Stroke = new SolidColorBrush(color),
                    //StrokeThickness = 3,
                    Height = thickness,
                    Width = thickness,
                    Fill = new SolidColorBrush(color),
                   
                    Margin = new Thickness(points[i].X - (thickness / 2.0), points[i].Y - (thickness / 2.0), 0, 0) // Sets the position.
                };
                canvas.Children.Add(point);
            }
            return canvas.Children.Count;
        }

        public void DrawString(Canvas canvas, string value, CartesianCore.Point point, double height, Color color, Typeface typeface)
        {

            var ts = new TextShape(value, typeface, height, color, (float)point.X + 5, (float)point.Y);
            canvas.Children.Add(ts);
        }       
    }
}

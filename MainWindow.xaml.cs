using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string path = "pt"; const string st = "el"; const string arr = "cr";
        string index = arr;
        bool paint = false;
        Point px, py = new Point();
        Pen pen = new Pen(Brushes.Black, 5);
        DrawingBrush brush = new DrawingBrush();
        Geometry geometry;
        PathFigure currentFigure;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            index = path;
        }

        private void btnStation_Click(object sender, RoutedEventArgs e)
        {
            index = st;

        }

        private void btnCursor_Click(object sender, RoutedEventArgs e)
        {
            index = arr;
            //GeometryDrawingExample();
        }


        void StartFigure(Point start)
        {
            currentFigure = new PathFigure() { StartPoint = start };
            var currentPath =
                new Path()
                {
                    Stroke = Brushes.Red,
                    StrokeThickness = 3,
                    Data = new PathGeometry() { Figures = { currentFigure } }
                };
            canDrawing.Children.Add(currentPath);
        }
        void AddFigurePoint(Point point)
        {
            currentFigure.Segments.Add(new LineSegment(point, isStroked: true));
        }
        void EndFigure()
        {
            currentFigure = null;
        }


        private void canDrawing_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!paint)
                return;
            AddFigurePoint(e.GetPosition(canDrawing));

        }

        private void canDrawing_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            py = e.GetPosition(this);
            AddFigurePoint(e.GetPosition(canDrawing));
            EndFigure();
            Mouse.Capture(null);
            paint = false;
        }

        private void canDrawing_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            px = e.GetPosition(this);
            Mouse.Capture(canDrawing);
            StartFigure(e.GetPosition(canDrawing));
        }
    }
}

using EditorSubwayMap.Model;
using System;
using System.Diagnostics;
using System.Net;
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
        enum ftype
        {
            N,
            line,
            ellipse,
            station
        }

        Line line;
        const int pt = 1, el = 2, st = 3 ;
        bool paint = false;
        Point px, py = new Point();
        ftype f;

        public MainWindow()
        {
            InitializeComponent();
            f = ftype.N;
        }

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            f = ftype.line;
            
        }

        private void btnStation_Click(object sender, RoutedEventArgs e)
        {
            f = ftype.station;
        }

        private void btnCursor_Click(object sender, RoutedEventArgs e)
        {
            f = ftype.N;
        }

        public Line DrawLine(Point startPoint, Point endPoint, Canvas canvas)
        {
            Line newEllipse = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness= 7
            };
            newEllipse.X1 = startPoint.X;
            newEllipse.Y1 = startPoint.Y;
            newEllipse.X2 = endPoint.X;
            newEllipse.Y2 = endPoint.Y;
            
            canvas.Children.Add(newEllipse);
            return newEllipse;
        }

        private void canDrawing_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!paint)
                return;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                line.X2 = e.GetPosition(canDrawing).X;
                line.Y2 = e.GetPosition(canDrawing).Y;
            }

        }

        private void canDrawing_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            py = e.GetPosition(canDrawing);
            paint = false;
        }

        private void canDrawing_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            px = e.GetPosition(canDrawing);
            paint = true;
            /*switch (f)
            {
                case ftype.N:
                    break;

                case ftype.line:
                    DrawLine dl = new DrawLine()
                    {
                        Pstart = px,
                        Pend = e.GetPosition(canDrawing),
                        color = Brushes.Black
                    };

                    line = dl.Draw();
                    canDrawing.Children.Add(line);

                    paint = true;
                    break;

                case ftype.ellipse:
                    break;
            }*/
            line = DrawLine(px, e.GetPosition(canDrawing), canDrawing);
        }
    }
}

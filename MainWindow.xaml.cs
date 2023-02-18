using EditorSubwayMap.DrawFigure;
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
        Ellipse ellipse;
        Label lable;

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
            paint = true;
        }

        private void btnStation_Click(object sender, RoutedEventArgs e)
        {
            f = ftype.station;
            paint = true;
        }

        private void btnCursor_Click(object sender, RoutedEventArgs e)
        {
            f = ftype.N;
            paint = false;
        }

        private void canDrawing_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && paint == true)
                switch (f)
                {
                    case ftype.N:
                        break;

                    case ftype.line:

                        line.X2 = e.GetPosition(canDrawing).X;
                        line.Y2 = e.GetPosition(canDrawing).Y;
                        break;

                    case ftype.station:
                        ellipse.Margin = new Thickness(
                            ellipse.Margin.Left, ellipse.Margin.Top,
                            ((int)canDrawing.ActualWidth) - e.GetPosition(canDrawing).X,
                            ((int)canDrawing.ActualHeight) - e.GetPosition(canDrawing).Y);
                        break;
                }
        }

        private void canDrawing_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            py = e.GetPosition(canDrawing);
            paint = false;
        }

        private void canDrawing_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (paint== true)
            {
                px = e.GetPosition(canDrawing);
                switch (f)
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

                    case ftype.station:
                        DrawStation ds = new DrawStation()
                        {
                            Pstart = px,
                            Pend = e.GetPosition(canDrawing),
                            color = Brushes.Black
                        };
                        ellipse = ds.Draw();
                        canDrawing.Children.Add(ellipse);
                        break;
                }
            }

        }
    }
}

// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
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

        DrawStation ds;
        DrawLine dl;

        public MainWindow()
        {
            InitializeComponent();
           // f = ftype.N;
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
                        ds.Pend = e.GetPosition(canDrawing);
                        ellipse = ds.Edit(ellipse);
                        break;
                }
        }

        private void canDrawing_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            px = e.GetPosition(canDrawing);
            paint = false;
        }

        private void canDrawing_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            paint = true;
            if (paint == true)
            {
                px = e.GetPosition(canDrawing);

                switch (f)
                {
                    case ftype.N:
                        break;

                    case ftype.line:
                        dl = new DrawLine()
                        {
                            Pstart = px,
                            Pend = px,
                            color = Brushes.Black
                        };
                        line = dl.Draw();
                        canDrawing.Children.Add(line);
                        break;

                    case ftype.station:
                        ds = new DrawStation()
                        {
                            Pstart = px,
                            color = Brushes.Black
                        };

                        ds.Pend = e.GetPosition(canDrawing);
                        ellipse = ds.Draw();
                        canDrawing.Children.Add(ellipse);
                        break;
                }
            }

        }
    }
}

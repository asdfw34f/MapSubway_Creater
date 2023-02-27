// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Model;
using System;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

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
        
        bool paint = false;
        Point px = new Point();
        ftype f;

        DrawEllipse de;
        DrawStation ds;
        DrawLine dl;

        public MainWindow()
        {
            InitializeComponent();
            labelY.Content = "Y: 0";
            labelX.Content = "X: 0";
        }

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            f = ftype.line;
            paint = true;
        }

        private void btnEllipse_Click(object sender, RoutedEventArgs e)
        {
            f = ftype.ellipse;
            paint = true;
        }

        private void btnCursor_Click(object sender, RoutedEventArgs e)
        {
            f = ftype.N;
            paint = false;
        }

        private void btnStation_Click(object sender, RoutedEventArgs e)
        {
            f = ftype.station;
            paint = true;
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

                        break;

                    case ftype.ellipse:
                        de.Pend = e.GetPosition(canDrawing);
                        ellipse = de.EditSize(ellipse);

                        break;
                }
            labelX.Content = "X: " + e.GetPosition(canDrawing).X;
            labelY.Content = "Y: " + e.GetPosition(canDrawing).Y;

           // labelFX.Content = canDrawing.Children. 
        }

        private void canDrawing_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            px = e.GetPosition(canDrawing);
            paint = false;
        }

        private void canDrawing_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            paint = true;

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
                        color = Brushes.Black,
                        Pend = e.GetPosition(canDrawing)
                    };

                    ellipse = ds.Draw();
                    canDrawing.Children.Add(ellipse);
                    break;

                case ftype.ellipse:
                    de = new DrawEllipse(canDrawing)
                    {
                        Pstart = px,
                        color = Brushes.Black
                    };

                    de.Pend = e.GetPosition(canDrawing);
                    ellipse = de.Draw();
                    canDrawing.Children.Add(ellipse);
                    break;
            }
        }
    }
}

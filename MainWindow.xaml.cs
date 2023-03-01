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

            de = new DrawEllipse(canDrawing);
            ds = new DrawStation(canDrawing);
            dl = new DrawLine(canDrawing);

            labelY.Content = "Y: 0";
            labelX.Content = "X: 0";
        }

        // BUTTONS

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            de.iditLoc = false;
            dl.iditLoc = false;
            ds.iditLoc = false;
            paint = true;

            f = ftype.line;
            Cursor = Cursors.Arrow;
        }

        private void btnEllipse_Click(object sender, RoutedEventArgs e)
        {
            de.iditLoc = false;
            dl.iditLoc = false;
            ds.iditLoc = false;
            paint = true;

            f = ftype.ellipse;
            Cursor = Cursors.Arrow;
        }

        private void btnCursor_Click(object sender, RoutedEventArgs e)
        {
            de.iditLoc = false;
            dl.iditLoc = false;
            ds.iditLoc = false;
            paint = false;

            f = ftype.N;
            Cursor = Cursors.Arrow;
        }

        private void btnStation_Click(object sender, RoutedEventArgs e)
        {
            de.iditLoc = false;
            dl.iditLoc = false;
            ds.iditLoc = false;
            paint = true;

            f = ftype.station;
            Cursor = Cursors.Arrow;
        }

        private void BtnCursorMove_Click(object sender, RoutedEventArgs e)
        {
            de.iditLoc = true;
            dl.iditLoc = true;
            ds.iditLoc = true;

            f = ftype.N;
            Cursor = Cursors.SizeAll;
        }

        // CANVAS MOUSE EVENTS

        private void canDrawing_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && paint == true)
                switch (f)
                {
                    case ftype.N:
                        break;

                    //  DRAW LINE 
                    case ftype.line:

                        line.X2 = e.GetPosition(canDrawing).X;
                        line.Y2 = e.GetPosition(canDrawing).Y;
                        break;

                    //  DRAW STATION
                    case ftype.station:
                        break;

                    //  DRAW ELLIPSE
                    case ftype.ellipse:

                        de.Pend = e.GetPosition(canDrawing);
                        ellipse = de.EditSize(ellipse);
                        break;
                }

            labelX.Content = "X: " + e.GetPosition(canDrawing).X;
            labelY.Content = "Y: " + e.GetPosition(canDrawing).Y;
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

                    //  DRAW LINE 
                case ftype.line:
                    
                    dl.Pstart = px;
                    dl.Pend = px;
                    dl.color = Brushes.Black;

                    line = dl.Draw();
                    canDrawing.Children.Add(line);
                    break;

                    //  DRAW STATION
                case ftype.station:

                    ds.Pstart = px;
                    ds.color = Brushes.Black;

                    ellipse = ds.Draw();
                    canDrawing.Children.Add(ellipse);
                    break;

                    //  DRAW ELLIPSE
                case ftype.ellipse:

                    de.Pstart = px;
                    de.color = Brushes.Black;

                    de.Pend = e.GetPosition(canDrawing);
                    ellipse = de.Draw();
                    canDrawing.Children.Add(ellipse);
                    break;
            }
        }
    }
}
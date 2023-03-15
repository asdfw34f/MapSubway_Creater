// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using EditorSubwayMap.Atributs;
using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Path = System.IO.Path;

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

        struct Atr_St
        {
            public string Name;
            public int idLine;
            public int backWay;
            public int NextWay;
        }

        Line line;

        Ellipse ellipse;

        bool paint = false;

        Point px = new Point();

        ftype f;
        Atr_St St;

        DrawEllipse de;
        DrawStation ds;
        DrawLine dl;

        BrushConverter conv;


        Brush br;

        List<Station> stations;

        public MainWindow()
        {
            InitializeComponent();
            conv = new BrushConverter(); 
            stations = new List<Station>();
            var values = typeof(Brushes).GetProperties().
                Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).
                ToArray();
            cboColors.ItemsSource = values;
            cboColors.SelectedIndex= 7;
                

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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOpenMap_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            canDrawing.Children.RemoveRange(0, canDrawing.Children.Count);
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

            switch (f)
            {
                case ftype.N:
                    break;

                //  LINE 
                case ftype.line:

                    break;

                //  STATION
                case ftype.station:

                    Station station = new Station(St.Name, St.NextWay,
                        St.backWay, St.idLine);

                    stations.Add(station);

                    break;

                //  ELLIPSE
                case ftype.ellipse:
                    break;
            }
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

                    string col = cboColors.SelectedValue as string;
                    dl.color = conv.ConvertFromString(col) as Brush;
                    

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
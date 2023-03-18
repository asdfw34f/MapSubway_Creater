// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using DrawMapMetroLibrary.Saving;
using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        ftype f;

        DrawEllipse de;
        DrawStation ds;
        DrawLine dl;

        SaveStation station;
        SaveLineWay lineWay;
        SaveEllipseWay ellipseWay;

        Line line;
        Ellipse ellipse;

        Brush brush;
        BrushConverter conv;
        string col;

        Point px = new Point();
        bool paint = false;

        public MainWindow()
        {
            InitializeComponent();
            
            conv = new BrushConverter(); 
            var values = typeof(Brushes).GetProperties().
                Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).
                ToArray();
            cboColors.ItemsSource = values;
            cboColors.SelectedIndex= 7;
            col = cboColors.SelectedValue as string;

            de = new DrawEllipse(canDrawing);
            ds = new DrawStation(canDrawing);
            dl = new DrawLine(canDrawing);

            labelY.Content = "Y: 0";
            labelX.Content = "X: 0";

            station = new SaveStation();
            lineWay = new SaveLineWay();
            ellipseWay = new SaveEllipseWay();
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

        private void cboColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            col = cboColors.SelectedValue as string;
            brush = conv.ConvertFromString(col) as Brush;
        }

        private void AtrSt_NextSt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.Text = null;
        }

        private void AtrSt_BackSt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.Text = null;
        }

        private void BtnAddStation_MouseUp(object sender, MouseButtonEventArgs e)
        {
            station.AddStation(AtrSt_NextSt.Text, Convert.ToInt32(AtrSt_BackWay.Text),
                Convert.ToInt32(AtrSt_NextWay.Text), AtrSt_NameWay.Text, brush, ds.Pstart);
        }

        private void AWay_Name_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.Text = null;
        }

        private void BtnAddWay_MouseUp(object sender, MouseButtonEventArgs e)
        {

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

                    if (AtrSt_grid.IsVisible)
                    {
                        AtrSt_grid.Visibility = Visibility.Hidden;
                    }
                    AtrWay_grid.Visibility = Visibility.Visible;
                    break;

                //  STATION
                case ftype.station:
                    if (AtrWay_grid.IsVisible)
                    {
                        AtrWay_grid.Visibility = Visibility.Hidden;
                    }
                    AtrSt_grid.Visibility = Visibility.Visible;
                    break;

                //  ELLIPSE
                case ftype.ellipse:

                    if (AtrSt_grid.IsVisible)
                    {
                        AtrSt_grid.Visibility = Visibility.Hidden;
                    }
                    AtrWay_grid.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void canDrawing_MouseDown(object sender, MouseButtonEventArgs e)
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
                    dl.color = conv.ConvertFromString(col) as Brush;

                    line = dl.Draw();
                    canDrawing.Children.Add(line);
                    break;

                //  DRAW STATION
                case ftype.station:

                    ds.Pstart = px;
                    ds.color = conv.ConvertFromString(col) as Brush;

                    ellipse = ds.Draw();
                    canDrawing.Children.Add(ellipse);
                    break;

                //  DRAW ELLIPSE
                case ftype.ellipse:

                    de.Pstart = px;
                    de.color = conv.ConvertFromString(col) as Brush;
                    de.Pend = e.GetPosition(canDrawing);

                    ellipse = de.Draw();
                    canDrawing.Children.Add(ellipse);
                    break;
            }
        }


    }
}
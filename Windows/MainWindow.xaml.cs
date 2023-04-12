// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using DrawMapMetroLibrary.Saving;
using System.Windows.Forms;
using Cursors = System.Windows.Input.Cursors;
using TextBox = System.Windows.Controls.TextBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using AtributsSubwauLibrary.Import;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum ftype { N, station, ellipse, line }
        ftype f;

        bool paint = false;

        DrawEllipse de;
        DrawStation ds;
        DrawLine dl;

        SaveStation station;
        SaveLineWay lineWay;
        SaveEllipseWay ellipseWay;

        Line line;
        Ellipse ellipse;

        List<Ellipse> sts;
        List<Ellipse> elWays;
        List<Line> lineWays;

        BrushConverter conv;
        string col;
        List<string> WayNames = new List<string>();
        
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

            AtrSt_NameWay.ItemsSource = WayNames;
            AtrSt_NameWay.SelectedIndex = 0;
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
            var folder = new FolderBrowserDialog();
            folder.ShowDialog();
            
            lineWay.Save(folder.SelectedPath);
            ellipseWay.Save(folder.SelectedPath);
            station.Save(folder.SelectedPath); 
        }

        private void btnOpenMap_Click(object sender, RoutedEventArgs e)
        {
            var folder = new FolderBrowserDialog();
            folder.ShowDialog();

            ImportStation importSt = new ImportStation();
            sts = importSt.Import(folder.SelectedPath);
            foreach (Ellipse st in sts)
            {
                canDrawing.Children.Add(st);
            }

            ImportEllipseWay importEl = new ImportEllipseWay(canDrawing);
            elWays = importEl.Import(folder.SelectedPath);
            foreach (Ellipse way in elWays)
            {
                canDrawing.Children.Add(way);
            }

            ImportLineWay importLn = new ImportLineWay();
            lineWays = importLn.Import(folder.SelectedPath);
            foreach (Line way in lineWays)
            {
                canDrawing.Children.Add(way);
            }
        }

        private void cboColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            col = cboColors.SelectedValue as string;
        }

        private void btnRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            canDrawing.Children.RemoveRange(0, canDrawing.Children.Count);
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

        private void AWay_Name_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.Text = null;
        }

        private void BtnAddStation_MouseUp(object sender, RoutedEventArgs e)
        {
            station.AddStation(
                AtrSt_NameSt.Text.ToString(), AtrSt_NameWay.Text.ToString(),
                Convert.ToInt16(AtrSt_BackWay.Text.ToString()), 
                Convert.ToInt16(AtrSt_NextWay.Text.ToString()),
                new Point(Canvas.GetLeft(ellipse), Canvas.GetTop(ellipse)),
                cboColors.SelectedValue.ToString());

            sts.Add(ellipse);
        }

        private void BtnAddWay_MouseUp(object sender, RoutedEventArgs e)
        {
            switch (f)
            {
                case ftype.N: break;
                case ftype.station: break;

                //  DRAW LINE 
                case ftype.line:
                    WayNames.Add(AWay_Name.Text);
                    lineWay.AddWay(AWay_Name.Text, new Point(line.X1, line.Y1),
                        new Point(line.X1, line.Y2), cboColors.SelectedValue.ToString());
                    lineWays.Add(line);
                    AWay_Name.Text = "Ветка добавлена";
                    break;

                //  DRAW ELLIPSE
                case ftype.ellipse:
                    WayNames.Add(AWay_Name.Text);
                    ellipseWay.AddWay(AWay_Name.Text, new Point(Canvas.GetLeft(ellipse), Canvas.GetTop(ellipse)),
                        cboColors.SelectedValue.ToString(), ellipse.Height, ellipse.Width);
                    
                    elWays.Add(ellipse);
                    AWay_Name.Text = "Ветка добавлена";
                    break;
            }
        }

        // CANVAS MOUSE EVENTS
        private void canDrawing_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && paint == true)
                switch (f)
                {
                    //  DRAW LINE 
                    case ftype.line:

                        line.X2 = e.GetPosition(canDrawing).X;
                        line.Y2 = e.GetPosition(canDrawing).Y;
                        break;

                    //  DRAW ELLIPSE
                    case ftype.ellipse:

                        de.currentP = e.GetPosition(canDrawing);
                        ellipse = de.EditSize(ellipse);
                        break;

                    default: break;
                }
            labelX.Content = "X: " + e.GetPosition(canDrawing).X;
            labelY.Content = "Y: " + e.GetPosition(canDrawing).Y;
        }

       private void canDrawing_MouseUp(object sender, MouseButtonEventArgs e)
       {
            paint = false;
            switch (f)
            {
                case ftype.N: break;

                //  STATION
                case ftype.station:
                    //if (canDrawing.IsMouseDirectlyOver)
                    {
                        ds.Pstart = e.GetPosition(canDrawing);
                        ds.color = conv.ConvertFromString(col) as Brush;

                        ellipse = ds.Draw();
                        canDrawing.Children.Add(ellipse);
                    }                    
                    if (AtrWay_grid.IsVisible)
                    {
                        AtrWay_grid.Visibility = Visibility.Hidden;
                    }
                    AtrSt_grid.Visibility = Visibility.Visible;
                break;

                case ftype.ellipse:
                    if (AtrSt_grid.IsVisible)
                    {
                        AtrSt_grid.Visibility = Visibility.Hidden;
                    }
                    AtrWay_grid.Visibility = Visibility.Visible;
                    break;

                case ftype.line:
                    if (AtrSt_grid.IsVisible)
                    {
                        AtrSt_grid.Visibility = Visibility.Hidden;
                    }
                    AtrWay_grid.Visibility = Visibility.Visible;
                    break;
                default: break;
            }
       }

        private void canDrawing_MouseDown(object sender, MouseButtonEventArgs e)
        {
            paint = true;
            switch (f)
            {
                case ftype.N:
                    break;

                //  DRAW LINE 
                case ftype.line:

                    AWay_Name.Text = "Название ветки: ";
                    dl.Pstart = e.GetPosition(canDrawing);
                    dl.Pend = e.GetPosition(canDrawing);
                    dl.color = conv.ConvertFromString(col) as Brush;

                    line = dl.Draw();
                    canDrawing.Children.Add(line);
                    break;

                //  DRAW STATION
                case ftype.station:
                    break;

                //  DRAW ELLIPSE
                case ftype.ellipse:
                    AWay_Name.Text = "Название ветки: ";

                    de.Pstart = e.GetPosition(canDrawing);
                    de.color = conv.ConvertFromString(col) as Brush;
                    de.currentP = e.GetPosition(canDrawing);

                    ellipse = de.Draw();
                    canDrawing.Children.Add(ellipse);
                    break;
            }
        }

        private void AtrWay_grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void AtrSt_grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
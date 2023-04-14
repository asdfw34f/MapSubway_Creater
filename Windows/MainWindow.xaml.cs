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
using System.Windows.Forms;
using Cursors = System.Windows.Input.Cursors;
using TextBox = System.Windows.Controls.TextBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using AtributsSubwauLibrary.Import;
using DrawMapMetroLibrary.Atributs;
using AtributsSubwauLibrary.Saving;
using System.Collections;
using static System.Windows.Forms.AxHost;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum ftype { N, station, ellipse, line }
        ftype f;

        string code = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        bool paint = false;

        DrawEllipse de;
        DrawStation ds;
        DrawLine dl;

        Line line;
        Ellipse ellipse;

        List<Station> sts;
        List<EllipseWay> elWays;
        List<LineWay> lineWays;

        List<Ellipse> elsts;
        List<Ellipse> eWays;
        List<Line> lWays;

        BrushConverter conv;
        string col;
        List<string> WayNames = new List<string>();

        int stID = 0;
        int wayID = 0;

        public MainWindow()
        {
            InitializeComponent();

            conv = new BrushConverter();
            var values = typeof(Brushes).GetProperties().
                Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).
                ToArray();
            cboColors.ItemsSource = values;
            cboColors.SelectedIndex = 7;
            col = cboColors.SelectedValue as string;

            de = new DrawEllipse();
            ds = new DrawStation();
            dl = new DrawLine();

            labelY.Content = "Y: 0";
            labelX.Content = "X: 0";

            AtrSt_NameWay.ItemsSource = WayNames;
            AtrSt_NameWay.SelectedIndex = 0;

            sts = new List<Station>();
            elWays = new List<EllipseWay>();
            lineWays = new List<LineWay>();
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

            foreach (UIElement element in canDrawing.Children )
            {
                int id = 0;
                if (element is Line)
                {
                    id = 0;
                    lWays.Add(element as Line);
                    foreach (LineWay lw in lineWays)
                    {
                        if (lw.WayID == (element as Line).Name)
                        {
                            lineWays[id].Start = new Point((element as Line).X1, (element as Line).Y1);
                            lineWays[id].End = new Point((element as Line).X2, (element as Line).Y2);
                            lineWays[id].Color = conv.ConvertToString((element as Line).Fill);
                        }
                        id++;
                    }
                }
                else if (element is Ellipse && (element as Ellipse).Width == 20)
                {
                    id = 0;
                    elsts.Add(element as Ellipse);
                    foreach(Station st in sts)
                    {
                        if (st.StationID == (element as Ellipse).Name)
                        {
                            sts[id].Position = new Point(
                                Canvas.GetLeft(element as Ellipse), 
                                Canvas.GetTop(element as Ellipse));
                            sts[id].Color = conv.ConvertToString((element as Ellipse).Fill);
                        }
                    }
                }
                else if (element is Ellipse)
                {
                    id = 0;
                    eWays.Add(element as Ellipse);
                    foreach (EllipseWay way in elWays)
                    {
                        if (way.WayID == (element as Ellipse).Name)
                        {
                            elWays[id].Position = new Point(
                                Canvas.GetLeft(element as Ellipse), 
                                Canvas.GetTop(element as Ellipse));
                            elWays[id].Width = (element as Ellipse).Width;
                            elWays[id].Height = (element as Ellipse).Height;
                            elWays[id].Color = conv.ConvertToString((element as Ellipse).Fill);
                        }
                    }
                }
            }

            SaveMap split = new SaveMap(sts, elWays, lineWays);
            split.Save(folder.SelectedPath);
        }

        private void btnOpenMap_Click(object sender, RoutedEventArgs e)
        {
            var folder = new FolderBrowserDialog();
            folder.ShowDialog();

            ImportMap map = new ImportMap();
            map.Import(folder.SelectedPath);
            
            List<Ellipse> ellipses = map.ellipses;
            if (ellipses != null)
            {
                foreach (Ellipse ellipse in ellipses)
                {
                    canDrawing.Children.Add(ellipse);
                }
                ellipses.Clear();
            }

            ellipses = map.stations;
            if (ellipses != null)
            {
                foreach (Ellipse ellipse in ellipses)
                {
                    canDrawing.Children.Add(ellipse);
                }
                ellipses.Clear();
            }

            List<Line> lines = map.lines;
            if (lines != null)
            {
                foreach (Line way in lines)
                {
                    canDrawing.Children.Add(way);
                }
                lines.Clear();
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
            sts.Add(
                new Station()
                {
                    StationID = code[stID].ToString() + code[stID++].ToString(),
                    NameStation = AtrSt_NameSt.Text.ToString(),
                    NameWay = AtrSt_NameWay.Text.ToString(),
                    Back = Convert.ToInt16(AtrSt_BackWay.Text.ToString()),
                    Go = Convert.ToInt16(AtrSt_NextWay.Text.ToString()),
                    Position = new Point(Canvas.GetLeft(ellipse), Canvas.GetTop(ellipse)),
                    Color = cboColors.SelectedValue.ToString()
                });
            ellipse.Name = code[stID].ToString() + code[stID++].ToString();
            ellipse.ToolTip = "Станция:    " + AtrSt_NameSt.Text;
            stID++;
        }

        private void BtnAddWay_MouseUp(object sender, RoutedEventArgs e)
        {
            switch (f)
            {
                case ftype.N:
                    break;
                case ftype.station:
                    break;

                //  DRAW LINE 
                case ftype.line:
                    WayNames.Add(AWay_Name.Text);

                    lineWays.Add(
                        new LineWay()
                        {
                            WayID = code[wayID].ToString() + code[wayID++].ToString(),
                            NameWay = AWay_Name.Text,
                            Start = new Point(line.X1, line.Y1),
                            End = new Point(line.X2, line.Y2),
                            Color = cboColors.SelectedValue.ToString()
                        });
                    line.ToolTip = "Ветка метро:    " + AWay_Name.Text;
                    line.Name = code[wayID].ToString() + code[wayID++].ToString();
                    AWay_Name.Text = "Ветка добавлена";
                    wayID++;
                    break;

                //  DRAW ELLIPSE
                case ftype.ellipse:
                    WayNames.Add(AWay_Name.Text);

                    elWays.Add(
                        new EllipseWay()
                        {
                            WayID = code[wayID].ToString() + code[wayID++].ToString(),
                            NameWay = AWay_Name.Text,
                            Position = new Point(Canvas.GetLeft(ellipse), Canvas.GetTop(ellipse)),
                            Color = cboColors.SelectedValue.ToString(),
                            Width = ellipse.Width,
                            Height = ellipse.Height
                        });
                    ellipse.Name = code[wayID].ToString() + code[wayID++].ToString();
                    ellipse.ToolTip = "Ветка метро:    " + AWay_Name.Text;
                    AWay_Name.Text = "Ветка добавлена";
                    wayID++;
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

                    default:
                        break;
                }
            labelX.Content = "X: " + e.GetPosition(canDrawing).X;
            labelY.Content = "Y: " + e.GetPosition(canDrawing).Y;
        }

        private void canDrawing_MouseUp(object sender, MouseButtonEventArgs e)
        {
            paint = false;
            switch (f)
            {
                case ftype.N:
                    break;

                //  STATION
                case ftype.station:
                    ds.Pstart = e.GetPosition(canDrawing);
                    ds.color = conv.ConvertFromString(col) as Brush;
                    ellipse = ds.Draw();
                    canDrawing.Children.Add(ellipse);
                    /*
                    Point cent = new Point(
                        Canvas.GetLeft(ellipse) - 0.5 * ellipse.Width,
                        Canvas.GetTop(ellipse) - 0.5 * ellipse.Height);

                    DragDrop.DoDragDrop(ellipse, ellipse, System.Windows.DragDropEffects.Link);*/

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
                default:
                    break;
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
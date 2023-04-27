// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using AtributsSubwauLibrary.Import;
using AtributsSubwauLibrary.Saving;
using DrawMapMetroLibrary.Atributs;
using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.model;
using EditorSubwayMap.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Cursors = System.Windows.Input.Cursors;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using TextBox = System.Windows.Controls.TextBox;

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

        DrawEllipse drawEllipse = new DrawEllipse();
        DrawStation drawStation = new DrawStation();
        DrawLine drawLine = new DrawLine();

        Line line;
        Ellipse ellipse;

        List<Station> stations = new List<Station>();
        List<EllipseWay> ellipseWays = new List<EllipseWay>();
        List<LineWay> lineWays = new List<LineWay>();

        List<Ellipse> elsts = new List<Ellipse>();
        List<Ellipse> eWays = new List<Ellipse>();
        List<Line> lWays = new List<Line>();

        BrushConverter conv;
        string col;
        List<string> WayNames = new List<string>();

        double OriginalCanvasWidth;
        double OriginalCanvasHeight;

        public MainWindow()
        {
            InitializeComponent();

            OriginalCanvasWidth = canDrawing.Width;
            OriginalCanvasHeight = canDrawing.Height;

            conv = new BrushConverter();
            var values = typeof(Brushes).GetProperties().
                Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).
                ToArray();
            cboColors.ItemsSource = values;
            cboColors.SelectedIndex = 7;
            col = cboColors.SelectedValue as string;

            labelY.Content = "Y: 0";
            labelX.Content = "X: 0";

            drawStation.canvas = canDrawing;
            drawEllipse.canvas = canDrawing;
            drawLine.canvas = canDrawing;

            AtrSt_NameWay.ItemsSource = WayNames;
            AtrSt_NameWay.SelectedIndex = 0;
            canDrawing.MouseWheel += canvas_MouseWheel;
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
                        drawEllipse.currentP = e.GetPosition(canDrawing);
                        ellipse = drawEllipse.EditSize(ellipse);
                        break;
                }
            labelX.Content = "X: " + e.GetPosition(canDrawing).X;
            labelY.Content = "Y: " + e.GetPosition(canDrawing).Y;
        }

        private void canDrawing_MouseUp(object sender, MouseButtonEventArgs e)
        {
            paint = false;
            if (f == ftype.N)
                return;
            if (f == ftype.line || f == ftype.ellipse)
            {
                if (AtrSt_grid.IsVisible)
                {
                    AtrSt_grid.Visibility = Visibility.Hidden;
                }
                AtrWay_grid.Visibility = Visibility.Visible;
            }
            else if (f == ftype.station)
            {
                drawStation.Pstart = e.GetPosition(canDrawing);
                drawStation.color = conv.ConvertFromString(col) as Brush;
                ellipse = drawStation.Draw();
                canDrawing.Children.Add(ellipse);

                if (AtrWay_grid.IsVisible)
                {
                    AtrWay_grid.Visibility = Visibility.Hidden;
                }
                AtrSt_grid.Visibility = Visibility.Visible;

                modelStation ms = new modelStation(ellipse, lWays, eWays);
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
                    drawLine.Pstart = e.GetPosition(canDrawing);
                    drawLine.Pend = e.GetPosition(canDrawing);
                    drawLine.color = conv.ConvertFromString(col) as Brush;

                    line = drawLine.Draw();
                    canDrawing.Children.Add(line);
                    break;

                //  DRAW STATION
                case ftype.station:
                    break;

                //  DRAW ELLIPSE
                case ftype.ellipse:
                    AWay_Name.Text = "Название ветки: ";
                    drawEllipse.Pstart = e.GetPosition(canDrawing);
                    drawEllipse.color = conv.ConvertFromString(col) as Brush;
                    drawEllipse.currentP = e.GetPosition(canDrawing);

                    ellipse = drawEllipse.Draw();
                    canDrawing.Children.Add(ellipse);
                    break;
            }
        }

        // BUTTONS

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            paint = true;
            f = ftype.line;
            Cursor = Cursors.Arrow;
        }

        private void btnEllipse_Click(object sender, RoutedEventArgs e)
        {
            paint = true;
            f = ftype.ellipse;
            Cursor = Cursors.Arrow;
        }

        private void btnCursor_Click(object sender, RoutedEventArgs e)
        {
            paint = false;
            f = ftype.N;
            Cursor = Cursors.Arrow;
        }

        private void btnStation_Click(object sender, RoutedEventArgs e)
        {
            paint = true;
            f = ftype.station;
            Cursor = Cursors.Arrow;
        }

        private void BtnCursorMove_Click(object sender, RoutedEventArgs e)
        {
            f = ftype.N;
            Cursor = Cursors.SizeAll;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var folder = new FolderBrowserDialog();
            folder.ShowDialog();

            foreach (UIElement element in canDrawing.Children)
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
                    foreach (Station st in stations)
                    {
                        if (st.StationID == (element as Ellipse).Name)
                        {
                            stations[id].Position = new Point(
                                Canvas.GetLeft(element as Ellipse),
                                Canvas.GetTop(element as Ellipse));
                            stations[id].Color = conv.ConvertToString((element as Ellipse).Fill);
                        }
                    }
                }
                else if (element is Ellipse)
                {
                    id = 0;
                    eWays.Add(element as Ellipse);
                    foreach (EllipseWay way in ellipseWays)
                    {
                        if (way.WayID == (element as Ellipse).Name)
                        {
                            ellipseWays[id].Position = new Point(
                                Canvas.GetLeft(element as Ellipse),
                                Canvas.GetTop(element as Ellipse));
                            ellipseWays[id].Width = (element as Ellipse).Width;
                            ellipseWays[id].Height = (element as Ellipse).Height;
                            ellipseWays[id].Color = conv.ConvertToString((element as Ellipse).Fill);
                        }
                    }
                }
            }
            SaveMap split = new SaveMap(stations, ellipseWays, lineWays);
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

            List<Line> lines = map.lines;
            if (lines != null)
            {
                foreach (Line way in lines)
                {
                    canDrawing.Children.Add(way);
                }
                lines.Clear();
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
        }

        private void BtnAddStation_MouseUp(object sender, RoutedEventArgs e)
        {
            stations.Add(
                new Station()
                {
                    StationID = AtrSt_NameWay.Text.ToString() + AtrSt_NameSt.Text.ToString(),
                    NameStation = AtrSt_NameSt.Text.ToString(),
                    NameWay = AtrSt_NameWay.Text.ToString(),
                    Back = Convert.ToInt16(AtrSt_BackWay.Text.ToString()),
                    Go = Convert.ToInt16(AtrSt_NextWay.Text.ToString()),
                    Position = new Point(Canvas.GetLeft(ellipse), Canvas.GetTop(ellipse)),
                    Color = cboColors.SelectedValue.ToString()
                });
            ellipse.Name = AtrSt_NameWay.Text.ToString() + AtrSt_NameSt.Text.ToString();
            ellipse.ToolTip = "Станция: " + AtrSt_NameSt.Text;
            success.Visibility = Visibility.Visible;
        }

        private void BtnAddWay_MouseUp(object sender, RoutedEventArgs e)
        {
            switch (f)
            {
                //  DRAW LINE 
                case ftype.line:
                    WayNames.Add(AWay_Name.Text);

                    lineWays.Add(
                        new LineWay()
                        {
                            WayID = AtrSt_NameWay.Text.ToString() + "ID",
                            NameWay = AWay_Name.Text,
                            Start = new Point(line.X1, line.Y1),
                            End = new Point(line.X2, line.Y2),
                            Color = cboColors.SelectedValue.ToString()
                        });
                    line.ToolTip = "Ветка метро: " + AWay_Name.Text;
                    line.Name = AtrSt_NameWay.Text.ToString();
                    AWay_Name.Text = "Ветка добавлена";
                    lWays.Add(line);
                    break;

                //  DRAW ELLIPSE
                case ftype.ellipse:
                    WayNames.Add(AWay_Name.Text);

                    ellipseWays.Add(
                        new EllipseWay()
                        {
                            WayID = AtrSt_NameWay.Text.ToString() + "ID",
                            NameWay = AWay_Name.Text,
                            Position = new Point(Canvas.GetLeft(ellipse), Canvas.GetTop(ellipse)),
                            Color = cboColors.SelectedValue.ToString(),
                            Width = ellipse.Width,
                            Height = ellipse.Height
                        });
                    ellipse.Name = AtrSt_NameWay.Text.ToString();
                    ellipse.ToolTip = "Ветка метро: " + AWay_Name.Text;
                    AWay_Name.Text = "Ветка добавлена";
                    eWays.Add(ellipse);
                    break;
            }
        }

        private void canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void cboColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            col = cboColors.SelectedValue as string;
        }

        private void btnRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            canDrawing.Children.RemoveRange(0, canDrawing.Children.Count);
        }

        private void AtrSt_NextSt_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((sender as TextBox).Text == "0")
            {
                (sender as TextBox).Text = null;
            }
        }

        private void AtrSt_BackSt_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((sender as TextBox).Text == "0")
            {
                (sender as TextBox).Text = null;
            }
        }

        private void AtrSt_NameSt_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((sender as TextBox).Text == "Название станции:")
            {
                (sender as TextBox).Text = null;
            }
        }

        private void AWay_Name_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((sender as TextBox).Text == "Название ветки:")
            {
                (sender as TextBox).Text = null;
            }
        }

        private void AtrSt_NameSt_MouseLeave(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty((sender as TextBox).Text))
            {
                (sender as TextBox).Text = "Название станции:";
            }
        }

        private void AtrSt_NextWay_MouseLeave(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty((sender as TextBox).Text))
            {
                (sender as TextBox).Text = "0";
            }
        }

        private void AtrSt_BackWay_MouseLeave(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty((sender as TextBox).Text))
            {
                (sender as TextBox).Text = "0";
            }
        }

        private void AWay_Name_MouseLeave(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty((sender as TextBox).Text))
            {
                (sender as TextBox).Text = "Название ветки:";
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

        private void AtrSt_NameSt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
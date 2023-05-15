using System;
using EditorSubwayMap.Infrastructure.Commands;
using EditorSubwayMap.MVVM.Base;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Windows;
using EditorSubwayMap.MVVM.Model;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Shapes;
using EditorSubwayMap.Infrastructure.IORoute;
using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Model;

namespace EditorSubwayMap.MVVM.ViewModel
{
    public class MainViewModel : NotifyPropertyChanged
    {
        public MainModel MainM;
      
        #region the Map
        #region Map and Properties 
        public Route Route
        {
            get => _route;
            set => Set(ref _route, value);
        }

        #region Fields
        private Brush _Color = Brushes.Black;
        private Array _Colors;
        #endregion

        #region Propertys 
        public Brush Color
        {
            get => _Color;
            set => Set(ref _Color, value);
        }
        #region User tools
        public string SelectedWay { get => _SelectedWay; set => Set(ref _SelectedWay, value); }
        public Array Colors { get => _Colors; private set => Set(ref _Colors, value); }
        #endregion

        


        #endregion
        #endregion

        #region Moduls
        private BrushConverter _ColorConvert = new BrushConverter();
        public List<Ellipse> Ellipses = new List<Ellipse>();
        public List<Line> Liness = new List<Line>();
        public List<Ellipse> Stationss = new List<Ellipse>();
        List<LineWay> Lines = new List<LineWay>();
        List<CircleWay> Circles = new List<CircleWay>();
        List<Station> Stations = new List<Station>();
        private Route _route = new Route();
        #endregion

        #region Shapes
        #region Fields
        private Point _point = new Point(0, 0);
        #endregion

        #region Figures
        public Ellipse Ellipse { get; set; } = new Ellipse();
        public Line Line { get; set; } = new Line();
        #endregion

        #region Adding to Map 
        public List<string> WayList
        {
            get => _WayList;
            set => Set(ref _WayList, value);
        }

        public string NameWay
        {
            get => _NameWay;
            set => Set(ref _NameWay, value);
        }

        public string ID
        {
            get => _ID; set => Set(ref _ID, value);
        }

        public string NameStation
        {
            get => _NameStation; set => Set(ref _NameStation, value);
        }

        public string DistanceNext
        {
            get => _distanceNext; set => Set(ref _distanceNext, value);
        }

        public string DistanceBack
        {
            get => _distanceBack; set => Set(ref _distanceBack, value);
        }

        public Point Point
        {
            get => _point; set => Set(ref _point, value);
        }
        #endregion
        #endregion

        #region Atributes
        #region Fields
        private List<string> _WayList = new List<string>() { };
        private string _SelectedWay;
        private string _ID = "we";
        private string _NameStation = "Название станции: ";
        private string _distanceNext = "0";
        private string _distanceBack = "0";
        private string _NameWay = "Название ветки: ";
        #endregion

        #region Grids
        #region Properties

        private Visibility _VisabilityStationGrid = Visibility.Hidden;
        public Visibility VisabilityStationGrid
        {
            get => _VisabilityStationGrid;
            set => Set(ref _VisabilityStationGrid, value);
        }
        private Visibility _VisabilityWayGrid = Visibility.Hidden;
        public Visibility VisabilityWayGrid
        {
            get => _VisabilityWayGrid;
            set => Set(ref _VisabilityWayGrid, value);
        }
        #endregion

        #region VisableChanged
        public ICommand CollapsedStation { get; }
        private bool CanCollapsedStation(object p) => true;
        private void OnCollapsedStation(object p)
        {
            NameWay = "Название станции:";
        }

        public ICommand CollapsedWay { get; }
        private bool CanCollapsedWay(object p) => true;
        private void OnCollapsedWay(object p)
        {
            NameStation = "Название ветки:";
            DistanceBack = "0";
            DistanceNext = "0";
        }
        #endregion
        #endregion

        #region Commands Adding the atributes
        public ICommand SaveWay { get; }
        private bool CanSaveWay(object p) => true;
        private void OnSaveWay(object p)
        {
            if (Drawing == Modes.Line)
            {
                Lines.Add(
                    new LineWay()
                    {
                        Color = _ColorConvert.ConvertToString(Color),
                        endPoint = new Point(
                            Line.X2, Line.Y2),
                        NameWay = NameWay,
                        startPoint = new Point(
                            Line.X1, Line.Y1),
                        stations = new List<Station>()
                    });

                WayList.Add(NameWay);

                Line.ToolTip = "Ветка: " + NameWay;
                Line.Name = NameWay;
                NameWay = "Ветка добавлена";
            }
            else if (Drawing == Modes.Circle)
            {
                Circles.Add(new CircleWay()
                {
                    Color = _ColorConvert.ConvertToString(Color),
                    Position = new Point(
                        Canvas.GetLeft(Ellipse), Canvas.GetTop(Ellipse)),
                    Height = Ellipse.Height,
                    Width = Ellipse.Width,
                    NameWay = NameWay,
                    stations = new List<Station>()
                });
                WayList.Add(NameWay);

                Ellipse.ToolTip = "Ветка: " + NameWay;
                Ellipse.Name = NameWay;
                NameWay = "Ветка добавлена";
            }
        }

        public ICommand SaveStation { get; }
        private bool CanSaveStation(object p) => true;
        private void OnSaveStation(object p)
        {
            Stations.Add(new Station()
            {
                Name = NameStation,
                NameWay = NameWay,
                Color = _ColorConvert.ConvertToString(Color),
                Position = new Point(
                    Canvas.GetLeft(Ellipse), Canvas.GetTop(Ellipse)),
                distanceBack = Convert.ToInt32(DistanceBack),
                distanceLast = Convert.ToInt32(DistanceNext)
            });
            Ellipse.ToolTip = "Станция - " + NameStation;
            NameStation = "Станция добалвена";
        }
        #endregion
        #endregion
        #endregion

        #region IO Map
        #region Command Save the Map
        public ICommand SaveMap { get; }
        private bool CanSaveMap(object p) => true;
        private void OnSaveMap(object p)
        {
            Route = new Route()
            {
                circleWays = Circles,
                lineWays = Lines,
                stations = Stations
            };

            SaveMap save = new SaveMap(Route);
            var folder = new FolderBrowserDialog();
            folder.ShowDialog();
            save.Save(folder.SelectedPath);
        }
        #endregion

        #region Command Import the Map
        public ICommand ImportMap { get; }
        private bool CanImportMap(object p) => true;
        private void OnImportMap(object p)
        {
            var folder = new FolderBrowserDialog();
            folder.ShowDialog();

            ImportMap map = new ImportMap();
            Route = map.Import(folder.SelectedPath);
            foreach (Line line in map.lines)
            {
                (p as Canvas).Children.Add(line);
            }
            foreach (Ellipse ellipse in map.ellipses)
            {
                (p as Canvas).Children.Add(ellipse);
            }
            foreach (Ellipse ellipse in map.stations)
            {
                (p as Canvas).Children.Add(ellipse);
            }
        }
        #endregion
        #endregion

        #region CANVAS
        #region Select a mode drawing
        public enum Modes
        {
            None,
            Circle,
            Line,
            Station
        }
        public Modes Drawing { get; set; }

        public ICommand SelectDrawLineCommand { get; }
        private bool CanSelectDrawLineCommand(object p) => true;
        private void OnSelectDrawLineCommand(object p) => Drawing = Modes.Line;

        public ICommand SelectDrawCircleCommand { get; }
        private bool CanSelectDrawCircleCommand(object p) => true;
        private void OnSelectDrawCircleCommand(object p) => Drawing = Modes.Circle;

        public ICommand SelectDrawStationCommand { get; }
        private bool CanSelectDrawStationCommand(object p) => true;
        private void OnSelectDrawStationCommand(object p) => Drawing = Modes.Station;

        public ICommand SelectDrawNoneCommand { get; }
        private bool CanSelectDrawNoneCommand(object p) => true;
        private void OnSelectDrawNoneCommand(object p) => Drawing = Modes.None;
        #endregion

        #region Run Drawing
        private bool _IsDrawing;

        public bool IsDrawing
        {
            get => _IsDrawing;
            set => Set(ref _IsDrawing, value);
        }
        #endregion

        #region Moduls
        DrawEllipse DCircle = new DrawEllipse();
        DrawLine DLine = new DrawLine();
        DrawStation DStation = new DrawStation();
        #endregion

        #region Figures
        private Ellipse _Circle = new Ellipse();
        private Line _Line = new Line();
        #endregion

        #region Mouse position
        private string _positionx = "0";
        public string PositionX
        {
            get => _positionx;
            set => Set(ref _positionx, value);
        }

        private string _positiony = "0";
        public string PositionY
        {
            get => _positiony;
            set => Set(ref _positiony, value);
        }
        #endregion

        #region Commands Mouse events / Drawing
        public ICommand MouseUp { get; }
        private bool CanMouseUp(object p) => true;
        private void OnMouseUp(object p)
        {
            IsDrawing = false;

            switch (Drawing)
            {
                case Modes.None:
                    break;
                case Modes.Station:
                {
                    DStation.Pstart = Mouse.GetPosition(p as Canvas);
                    DStation.color = Color;
                    _Circle = DStation.Draw();
                    (p as Canvas).Children.Add(_Circle);
                    Stationss.Add(_Circle);

                    if (VisabilityWayGrid == Visibility.Visible)
                    {
                        VisabilityWayGrid = Visibility.Collapsed;
                    }
                    VisabilityStationGrid = Visibility.Visible;
                    break;
                }
                case Modes.Circle:
                {
                    if (VisabilityStationGrid == Visibility.Visible)
                    {
                        VisabilityStationGrid = Visibility.Collapsed;
                    }
                    VisabilityWayGrid = Visibility.Visible;
                    break;
                }
                case Modes.Line:
                {
                    if (VisabilityStationGrid == Visibility.Visible)
                    {
                        VisabilityStationGrid = Visibility.Collapsed;
                    }
                    VisabilityWayGrid = Visibility.Visible;
                    break;
                }
            }
        }

        public ICommand MouseDown { get; }
        private bool CanMouseDown(object p) => true;
        private void OnMouseDown(object p)
        {
            IsDrawing = true;
            switch (Drawing)
            {
                case Modes.None:
                    break;
                //  DRAW STATION
                case Modes.Station:
                    break;
                //  DRAW LINE 
                case Modes.Line:
                {
                    DLine.Pstart = Mouse.GetPosition(p as Canvas);
                    DLine.Pend = Mouse.GetPosition(p as Canvas);
                    DLine.color = Color;
                    _Line = DLine.Draw();
                    (p as Canvas).Children.Add(_Line);
                    Liness.Add(_Line);
                    break;
                }
                //  DRAW ELLIPSE
                case Modes.Circle:
                {
                    DCircle.Pstart = Mouse.GetPosition(p as Canvas);
                    DCircle.color = Color;
                    DCircle.currentP = Mouse.GetPosition(p as Canvas);

                    _Circle = DCircle.Draw();
                    (p as Canvas).Children.Add(_Circle);
                    Ellipses.Add(_Circle);
                    break;

                }
            }
        }

        public ICommand MouseMove { get; }
        private bool CanMouseMoved(object p) => true;
        private void OnMouseMove(object p)
        {
            if (IsDrawing == true)
                switch (Drawing)
                {
                    case Modes.None:
                        break;
                    case Modes.Station:
                        break;
                    //  DRAW LINE 
                    case Modes.Line:
                    {
                        //ViewModel   //View
                        _Line.X2 = Mouse.GetPosition(p as Canvas).X;
                        _Line.Y2 = Mouse.GetPosition(p as Canvas).Y;
                        break;
                    }
                    //  DRAW ELLIPSE
                    case Modes.Circle:
                    {
                        DCircle.currentP = Mouse.GetPosition(p as Canvas);
                        _Circle = DCircle.EditSize(_Circle);
                        break;
                    }
                }
            PositionX = "X: " + Mouse.GetPosition(p as Canvas).X;
            PositionY = "Y: " + Mouse.GetPosition(p as Canvas).Y;
        }
        #endregion
        #endregion

        public MainViewModel()
        {
            MainM = new MainModel();

            SaveWay = new LambdaCommand(OnSaveWay, CanSaveWay);
            SaveStation = new LambdaCommand(OnSaveStation, CanSaveStation);
            SaveMap = new LambdaCommand(OnSaveMap, CanSaveMap);
            ImportMap = new LambdaCommand(OnImportMap, CanImportMap);

            CollapsedStation = new LambdaCommand(OnCollapsedStation, CanCollapsedStation);
            CollapsedWay = new LambdaCommand(OnCollapsedWay, CanCollapsedWay);

            SelectDrawLineCommand = new LambdaCommand(OnSelectDrawLineCommand, CanSelectDrawLineCommand);
            SelectDrawCircleCommand = new LambdaCommand(OnSelectDrawCircleCommand, CanSelectDrawCircleCommand);
            SelectDrawStationCommand = new LambdaCommand(OnSelectDrawStationCommand, CanSelectDrawStationCommand);
            SelectDrawNoneCommand = new LambdaCommand(OnSelectDrawNoneCommand, CanSelectDrawNoneCommand);

            MouseMove = new LambdaCommand(OnMouseMove, CanMouseMoved);
            MouseDown = new LambdaCommand(OnMouseDown, CanMouseDown);
            MouseUp = new LambdaCommand(OnMouseUp, CanMouseUp);

            Colors = typeof(Brushes).GetProperties()
            .Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).ToArray();
        }
    }
}
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

namespace EditorSubwayMap.MVVM.ViewModel
{
    public class MainViewModel : NotifyPropertyChanged
    {
        #region the Map
        List<LineWay> Lines = new List<LineWay>();
        List<CircleWay> Circles = new List<CircleWay>();
        List<Station> Stations = new List<Station>();
        private Route _route = new Route();
        public List<UIElement> Children;
        #endregion

        #region Shapes
        public Ellipse Ellipse { get; set; } = new Ellipse();
        public Line Line { get; set; } = new Line();
        #endregion

        #region Fields
        private BrushConverter _ColorConvert = new BrushConverter();
        private List<string> _WayList = new List<string>() { "erwer", "sdfy", "Werwer" };
        private string _SelectedWay;
        private Brush _Color = Brushes.Black;
        private Array _Colors;
        private string _ID = "we";
        private string _NameStation = "Название станции: ";
        private string _distanceNext = "0";
        private string _distanceBack = "0";
        private Point _point = new Point(0, 0);
        private string _NameWay = "Название ветки: ";
        private Visibility _VisabilityWayGrid = Visibility.Hidden;
        private Visibility _VisabilityStationGrid = Visibility.Hidden;
        #endregion

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
            DistanceNext= "0";
        }

        public Visibility VisabilityStationGrid
        {
            get => _VisabilityStationGrid;
            set => Set(ref _VisabilityStationGrid, value);
        }

        public Visibility VisabilityWayGrid
        {
            get => _VisabilityWayGrid;
            set => Set(ref _VisabilityWayGrid, value);
        }

        public Route Route
        {
            get => _route;
            set => Set(ref _route, value);
        }

        #region Adding to Map 

        public List<string> WayList
        {
            get => _WayList;
            set=> Set(ref _WayList, value);
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

        #region User tools
        public string SelectedWay { get => _SelectedWay; set=> Set(ref _SelectedWay, value); }
        public Array Colors {  get => _Colors; private set => Set(ref _Colors, value); }
        #endregion

        #region Map propertys 
        public Brush Color
        {
            get => _Color;
            set => Set(ref _Color, value);
        }
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
                    Color= _ColorConvert.ConvertToString(Color),
                    Position= new Point(
                        Canvas.GetLeft(Ellipse), Canvas.GetTop(Ellipse)),
                    Height = Ellipse.Height,
                    Width = Ellipse.Width,
                    NameWay= NameWay,
                    stations= new List<Station>()
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
                NameWay= NameWay,
                Color= _ColorConvert.ConvertToString(Color), 
                Position= new Point(
                    Canvas.GetLeft(Ellipse), Canvas.GetTop(Ellipse)),
                distanceBack = Convert.ToInt32(DistanceBack),
                distanceLast = Convert.ToInt32(DistanceNext)
            });
            Ellipse.ToolTip = "Станция - " + NameStation;
            NameStation = "Станция добалвена";
        }
        #endregion

        #region Command Save the Map
        public ICommand SaveMap{ get; }
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

        }
        #endregion

        public MainModel MainM;
        public MainViewModel()
        {
            MainM = new MainModel();

            SaveWay = new LambdaCommand(OnSaveWay, CanSaveWay);
            SaveStation = new LambdaCommand(OnSaveStation, CanSaveStation);
            SaveMap = new LambdaCommand(OnSaveMap, CanSaveMap);

            CollapsedStation = new LambdaCommand(OnCollapsedStation, CanCollapsedStation);
            CollapsedWay = new LambdaCommand(OnCollapsedWay, CanCollapsedWay);

            SelectDrawLineCommand = new LambdaCommand(OnSelectDrawLineCommand, CanSelectDrawLineCommand);
            SelectDrawCircleCommand = new LambdaCommand(OnSelectDrawCircleCommand, CanSelectDrawCircleCommand);
            SelectDrawStationCommand = new LambdaCommand(OnSelectDrawStationCommand, CanSelectDrawStationCommand);
            SelectDrawNoneCommand = new LambdaCommand(OnSelectDrawNoneCommand, CanSelectDrawNoneCommand);
            
            Colors = typeof(Brushes).GetProperties()
            .Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).ToArray();
        }
    }
}
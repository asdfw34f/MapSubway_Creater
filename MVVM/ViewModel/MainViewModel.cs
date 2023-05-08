using System;
using EditorSubwayMap.Infrastructure.Commands.MainCommands;
using EditorSubwayMap.Infrastructure.Commands;
using EditorSubwayMap.MVVM.Base;
using System.Windows.Input;
using EditorSubwayMap.Data;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using EditorSubwayMap.MVVM.Model.Rout;
using System.Windows;
using EditorSubwayMap.MVVM.Model;
using LineWay = EditorSubwayMap.MVVM.Model.Rout.LineWay;
using System.Windows.Controls;

namespace EditorSubwayMap.MVVM.ViewModel
{
    public class MainViewModel : NotifyPropertyChanged
    {
        #region Commands
        public SelectModeDrawing SelectModeCommands { get; } = new SelectModeDrawing();
        public IOMap IOMapCommands { get; } = new IOMap();
        public Saving SavingCommands { get; } = new Saving();
        #endregion
        
        #region the Map
        List<LineWay> Lines = new List<LineWay>();
        List<CircleWay> Circles = new List<CircleWay>();
        List<Station> Stations = new List<Station>();
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
        private string _NameWay = "Название ветки :";
        private Visibility _VisabilityWayGrid = Visibility.Hidden;
        private Visibility _VisabilityStationGrid = Visibility.Hidden;
        #endregion

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
            set { if (Set(ref _Color, value)) { OnCanvas.Color = _Color; } }
        }
        #endregion

        public ICommand SaveWay { get; }
        private bool CanSaveWay(object p) => true;
        private void OnSaveWay(object p)
        {
            if (OnCanvas.Drawing == OnCanvas.Modes.Line)
            {
                Lines.Add(
                    new LineWay()
                    {
                        Color = _ColorConvert.ConvertToString(Color),
                        endPoint = new Point(
                            OnCanvas.Line.X2, OnCanvas.Line.Y2),
                        NameWay = NameWay,
                        startPoint = new Point(
                            OnCanvas.Line.X1, OnCanvas.Line.Y1),
                        stations = new List<Station>()
                    });

                WayList.Add(NameWay);

                OnCanvas.Line.ToolTip = "Ветка: " + NameWay;
                OnCanvas.Line.Name = NameWay;
                NameWay = "Ветка добавлена";
            }
            else if (OnCanvas.Drawing == OnCanvas.Modes.Circle)
            {
                Circles.Add(new CircleWay()
                {
                    Color= _ColorConvert.ConvertToString(Color),
                    Position= new Point(
                        Canvas.GetLeft(OnCanvas.Ellipse), Canvas.GetTop(OnCanvas.Ellipse)),
                    Height = OnCanvas.Ellipse.Height,
                    Width = OnCanvas.Ellipse.Width,
                    NameWay= NameWay,
                    stations= new List<Station>()
                });
                WayList.Add(NameWay);

                OnCanvas.Ellipse.ToolTip = "Ветка: " + NameWay;
                OnCanvas.Ellipse.Name = NameWay;
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
                    Canvas.GetLeft(OnCanvas.Ellipse), Canvas.GetTop(OnCanvas.Ellipse)),
                distanceBack = Convert.ToInt32(DistanceBack),
                distanceLast = Convert.ToInt32(DistanceNext)
            });
            OnCanvas.Ellipse.ToolTip = "Станция - " + NameStation;
            NameStation = "Станция добалвена";
        }

        public MainModel MainM;
        public MainViewModel()
        {
            MainM = new MainModel();
            SaveWay = new LambdaCommand(OnSaveWay, CanSaveWay);
            SaveStation = new LambdaCommand(OnSaveStation, CanSaveStation);

            Colors = typeof(Brushes).GetProperties()
            .Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).ToArray();

        }
    }
}
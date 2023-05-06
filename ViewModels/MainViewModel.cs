using System;
using EditorSubwayMap.Infastructure.Commands;
using EditorSubwayMap.ViewModels.Base;
using System.Windows.Input;
using WpfApp1.Data;
using EditorSubwayMap.Models; 
using static EditorSubwayMap.Models.MainModel;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using AtributsSubwauLibrary.Model;
using RouteSubway = AtributsSubwauLibrary.Model.RouteSubway;
using System.Windows.Controls;

namespace EditorSubwayMap.ViewModels
{
    public class MainViewModel : ViewModel
    {
        /// <summary>
        /// public MainModel model = new MainModel();
        /// </summary>

        #region Fields
        private List<string> _WayList = new List<string>() { "erwer", "sdfy", "Werwer" };
        private RouteSubway _RouteSubway = new RouteSubway();
        private string _SelectedWay;
        private Brush _Color = Brushes.Black;
        private BrushConverter _ColorConvert = new BrushConverter();
        private Array _Colors;
        #endregion

        public string SelectedWay { get => _SelectedWay; set=> Set(ref _SelectedWay, value); }

        public Brush Color
        {
            get => _Color;
            set { if (Set(ref _Color, value)) { OnCanvas.Color = _Color; } }
        }

        /// <summary>
        /// List of Names of Ways, user saved
        /// </summary>
        public List<string> WayList { get => _WayList; set => Set(ref _WayList, value); }
        
        /// <summary>
        /// Binding Textbox some Station atributs
        /// </summary>
        public string NameStation
        {
            get => StationAtributs._NameStation;
            set => Set(ref StationAtributs._NameStation, value);
        }

        /// <summary>
        /// Binding Textbox some Station atributs
        /// </summary>
        public string distanceNext
        {
            get => StationAtributs._distanceNext;
            set => Set(ref StationAtributs._distanceNext, value);
        }
        
        /// <summary>
        /// Binding Textbox some Station atributs
        /// </summary>
        public string distanceBack
        {
            get => StationAtributs._distanceBack;
            set => Set(ref StationAtributs._distanceBack, value);
        }
        
        /// <summary>
        /// Binding Textbox some Way atributs
        /// </summary>
        public string NameWay { get => WayAtributs.NameWay; set => Set(ref WayAtributs.NameWay, value); }
        
        #region Commands Select the drawing mode 
        public ICommand SelectDrawLineCommand { get; }
        private bool CanSelectDrawLineCommand(object p) => true;
        private void OnSelectDrawLineCommand(object p) => OnCanvas.Drawing = OnCanvas.Modes.Line;

        public ICommand SelectDrawCircleCommand { get; }
        private bool CanSelectDrawCircleCommand(object p) => true;
        private void OnSelectDrawCircleCommand(object p) => OnCanvas.Drawing = OnCanvas.Modes.Circle;

        public ICommand SelectDrawStationCommand { get; }
        private bool CanSelectDrawStationCommand(object p) => true;
        private void OnSelectDrawStationCommand(object p) => OnCanvas.Drawing = OnCanvas.Modes.Station;

        public ICommand SelectDrawNoneCommand { get; }
        private bool CanSelectDrawNoneCommand(object p) => true;
        private void OnSelectDrawNoneCommand(object p) => OnCanvas.Drawing = OnCanvas.Modes.None;
        #endregion

        #region Map Commands

        public ICommand Save { get; }
        private bool CanSaved(object p) => true;
        private void SaveExecute(object p)
        {

        }

        public ICommand Import { get; }
        private bool CanImport(object p) => true;
        private void ImportExecute(object p)
        {

        }

        public ICommand RemoveAll { get; }
        private bool CanRemoveAll(object p) => true;
        private void OnRemoveAllExecute(object p)
        {

        }
        #endregion

        #region Commands Saving some Station and Ways
        public ICommand SaveStation { get; }
        private bool CanSaveStation(object p) => true;
        private void OnSaveStation(object p)
        {
            if (OnCanvas.Drawing == OnCanvas.Modes.Station)
            {
                foreach (var t in OnCanvas.RouteSubway.lineWays)
                {
                    if (t.NameWay == "")
                    {
                        t.stations.Add(
                            new Station
                            {
                                Name = NameStation,
                                distanceBack = Convert.ToInt32(distanceBack),
                                distanceLast = Convert.ToInt32(distanceNext),
                                Position = new System.Windows.Point(
                                    Canvas.GetLeft(OnCanvas.Ellipse),
                                    Canvas.GetTop(OnCanvas.Ellipse)),
                                Color = _ColorConvert.ConvertToString(Color),
                                StationID = "2",
                                NameWay= t.NameWay,
                            });
                        return;
                    }
                }

                foreach (var t in OnCanvas.RouteSubway.circleWays)
                {
                    if (t.NameWay== "")
                    {
                        t.stations.Add(
                            new Station
                            {
                                Name = NameStation,
                                distanceBack = Convert.ToInt32(distanceBack),
                                distanceLast = Convert.ToInt32(distanceNext),
                                Position = new System.Windows.Point(
                                    Canvas.GetLeft(OnCanvas.Ellipse),
                                    Canvas.GetTop(OnCanvas.Ellipse)),
                                StationID = "2",
                                NameWay= t.NameWay,
                                Color= _ColorConvert.ConvertToString(Color),
                            });
                        return;
                    }
                }
            }
        }

        public ICommand SaveLine { get; }
        private bool CanSaveLine(object p) => true;
        private void OnSaveLine(object p)
        {
            if (OnCanvas.Drawing == OnCanvas.Modes.Line)
            {
                OnCanvas.RouteSubway.lineWays.Add(
                    new LineWay()
                    {
                        Color = _ColorConvert.ConvertToString(Color),
                        endPoint = new System.Windows.Point(
                            OnCanvas.Line.X2, OnCanvas.Line.Y2),
                        NameWay = p.ToString(),
                        startPoint = new System.Windows.Point(
                            OnCanvas.Line.X1, OnCanvas.Line.Y1),
                        stations = null
                    });
            }
        }

        public ICommand SaveCircle { get; }
        private bool CanSaveCircle(object p) => true;
        private void OnSaveCircle(object p)
        {
            if (OnCanvas.Drawing == OnCanvas.Modes.Circle)
            {
                OnCanvas.RouteSubway.circleWays.Add(
                    new CircleWay()
                    {
                        Color = _ColorConvert.ConvertToString(Color),
                        Position = new System.Windows.Point(
                            Canvas.GetLeft(OnCanvas.Ellipse), 
                            Canvas.GetTop(OnCanvas.Ellipse)),
                        NameWay = p.ToString(),
                        Height = OnCanvas.Ellipse.Height,
                        Width = OnCanvas.Ellipse.Width,
                        stations = null
                    });
            }
        }
        #endregion

        public Array Colors
        {
            get => _Colors;
            private set => Set(ref _Colors, value);
        }

        public MainViewModel()
        {
            Colors = typeof(Brushes).GetProperties()
            .Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).ToArray();

            #region Map Commands
            Import = new LambdaCommand(ImportExecute, CanImport);
            Save = new LambdaCommand(SaveExecute, CanSaved);
            #endregion

            #region Commands Select the drawing mode 
            SelectDrawLineCommand = new LambdaCommand(OnSelectDrawLineCommand, CanSelectDrawLineCommand);
            SelectDrawCircleCommand = new LambdaCommand(OnSelectDrawCircleCommand, CanSelectDrawCircleCommand);
            SelectDrawStationCommand = new LambdaCommand(OnSelectDrawStationCommand, CanSelectDrawStationCommand);
            SelectDrawNoneCommand = new LambdaCommand(OnSelectDrawNoneCommand, CanSelectDrawNoneCommand);
            #endregion

            #region Commands Saving some Station and Ways
            //  SaveStation = new LambdaCommand(OnSaveStation, CanSaveStation);
            ////    SaveLine = new LambdaCommand(OnSaveLine, CanSaveLine);
            //    SaveCircle = new LambdaCommand(OnSaveCircle, CanSaveCircle);
            #endregion
        }
    }
}
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

namespace EditorSubwayMap.ViewModels
{
    public class MainViewModel : ViewModel
    {
        /// <summary>
        /// public MainModel model = new MainModel();
        /// </summary>

        #region Fields
        private string _Title = Convert.ToString(DrawingOnCanvas.Drawing);
        private List<string> _WayList = new List<string>() { "erwer", "sdfy", "Werwer" };
        private RouteSubway _RouteSubway = new RouteSubway();
        private int _IdStation = -1;
        private string _SColor;
        private Brush _Color = Brushes.Black;
        private BrushConverter _ColorConvert = new BrushConverter();
        private Array _Colors;
        #endregion

        public Brush Color
        {
            get => _Color;
            set => Set(ref _Color, value);
        }

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        /// <summary>
        /// List of Names of Ways, user saved
        /// </summary>
        public List<string> WayList
        {
            get =>_WayList;
            set => Set(ref _WayList, value);
        }
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
        public string NameWay
        {
            get => WayAtributs.NameWay;
            set => Set(ref WayAtributs.NameWay, value);
        }

        #region Commands Select the drawing mode 
        public ICommand SelectDrawLineCommand{ get; }
        private bool CanSelectDrawLineCommand(object p) => true;
        private void OnSelectDrawLineCommand(object p) => DrawingOnCanvas.Drawing = DrawingOnCanvas.Modes.Line;

        public ICommand SelectDrawCircleCommand { get; }
        private bool CanSelectDrawCircleCommand(object p) => true;
        private void OnSelectDrawCircleCommand(object p) => DrawingOnCanvas.Drawing = DrawingOnCanvas.Modes.Circle;

        public ICommand SelectDrawStationCommand { get; }
        private bool CanSelectDrawStationCommand(object p) => true;
        private void OnSelectDrawStationCommand(object p) => DrawingOnCanvas.Drawing = DrawingOnCanvas.Modes.Station;

        public ICommand SelectDrawNoneCommand { get; }
        private bool CanSelectDrawNoneCommand(object p) => true;
        private void OnSelectDrawNoneCommand(object p) => DrawingOnCanvas.Drawing = DrawingOnCanvas.Modes.None;
        #endregion

        #region Map Commands

        public ICommand Save { get; }
        private bool CanSaved(Object p) => true;

        private void SaveExecute(Object p)
        {
            
        }

        public ICommand Import { get; }
        private bool CanImport(Object p) => true;

        private void ImportExecute(Object p)
        {
            
        }

        public ICommand RemoveAll { get; }
        private bool CanRemoveAll(Object p) => true;

        private void OnRemoveAllExecute(Object p)
        {
            
        }
        #endregion

        #region Commands Saving some Station and Ways
        public ICommand SaveStation { get; }
        private bool CanSaveStation(object p) => true;
        /*    private void OnSaveStation(object p)
            {
                if (DrawingOnCanvas.Drawing == DrawingOnCanvas.Modes.Station)
                {
                    foreach (var t in model.RouteSubway.LineWays)
                    {
                        if (t.Name == "")
                        {
                            t.stations.Add(
                                new Station
                                {
                                    Name = NameStation,
                                    DistanceBack = Convert.ToInt32(distanceBack),
                                    DistanceLast = Convert.ToInt32(distanceNext),
                                    Position = new System.Windows.Point(
                                        Canvas.GetLeft(DrawingOnCanvas.Ellipse),
                                        Canvas.GetTop(DrawingOnCanvas.Ellipse)),
                                    StationId = model.IdStation++
                                });
                            return;
                        } 
                    }

                    foreach (var t in model.RouteSubway.CircleWays)
                    {
                        if (t.Name == "")
                        {
                            t.stations.Add(
                                new Station
                                {
                                    Name = NameStation,
                                    DistanceBack = Convert.ToInt32(distanceBack),
                                    DistanceLast = Convert.ToInt32(distanceNext),
                                    Position = new System.Windows.Point(
                                        Canvas.GetLeft(DrawingOnCanvas.Ellipse), 
                                        Canvas.GetTop(DrawingOnCanvas.Ellipse)),
                                    StationId = model.IdStation++
                                });
                            return;
                        }
                    }
                }
            }

            public ICommand SaveLine{ get; }
            private bool CanSaveLine(object p) => true;
            private void OnSaveLine(object p)
            {
                if (DrawingOnCanvas.Drawing == DrawingOnCanvas.Modes.Line)
                {
                    model.RouteSubway.LineWays.Add(
                        new LineWay()
                        {
                        });
                }
            }

            public ICommand SaveCircle{ get; }
            private bool CanSaveCircle(object p) => true;
            private void OnSaveCircle(object p)
            {
                if (DrawingOnCanvas.Drawing == DrawingOnCanvas.Modes.Circle)
                {

                }
            }*/
        #endregion

        public ICommand SelectBrush { get; }
        private bool CanSelectBrush(object p) => true;
        private void SelectBrushExecute(object p)
        {
            Color = (p as Brush);
        }

        public Array Colors
        {
            get => _Colors;
            private set => Set(ref _Colors, value);
        }

        public MainViewModel()
        {
            Colors = typeof(Brushes).GetProperties()
            .Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).ToArray();

            SelectBrush = new LambdaCommand(SelectBrushExecute, CanSelectBrush);

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
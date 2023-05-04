using System;
using EditorSubwayMap.Infastructure.Commands;
using EditorSubwayMap.ViewModels.Base;
using System.Windows.Input;
using WpfApp1.Data;
using EditorSubwayMap.Models;
using EditorSubwayMap.Models.ElementsOfMap;
using System.Windows.Controls;

namespace EditorSubwayMap.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainModel model = new MainModel();

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
        private void OnSaveStation(object p)
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
                                Name = model.NameStation,
                                DistanceBack = Convert.ToInt32(model.distanceBack),
                                DistanceLast = Convert.ToInt32(model.distanceNext),
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
                                Name = model.NameStation,
                                DistanceBack = Convert.ToInt32(model.distanceBack),
                                DistanceLast = Convert.ToInt32(model.distanceNext),
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
        }
        #endregion

        public MainViewModel()
        {
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
            SaveStation = new LambdaCommand(OnSaveStation, CanSaveStation);
            SaveLine = new LambdaCommand(OnSaveLine, CanSaveLine);
            SaveCircle = new LambdaCommand(OnSaveCircle, CanSaveCircle);
            #endregion
        }
    }
}
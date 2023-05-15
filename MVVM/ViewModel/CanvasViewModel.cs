using System.Windows.Controls;
using System.Windows.Input;
using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Model;
using System.Windows.Shapes;
using EditorSubwayMap.Infrastructure.Commands;
using EditorSubwayMap.MVVM.Base;
using EditorSubwayMap.MVVM.Model;
using System.Windows;

namespace EditorSubwayMap.MVVM.ViewModel
{
    public class CanvasViewModel : NotifyPropertyChanged
    {
        public CanvasModel DrawingBoard = new CanvasModel();
        public MainViewModel Main;

        #region Fields
        private bool _IsDrawing;
        #endregion
        
        public bool IsDrawing
        {
            get => _IsDrawing;
            set => Set(ref _IsDrawing, value);
        }

        #region Visability
        public Visibility VisabilityStationGrid
        {
            get => Main.VisabilityStationGrid;
            set=> Main.VisabilityStationGrid = value;
        }

        public Visibility VisabilityWayGrid
        {
            get => Main.VisabilityWayGrid;
            set => Main.VisabilityWayGrid = value;
        }
        #endregion

        #region Position
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

        DrawEllipse DCircle = new DrawEllipse();
        DrawLine DLine = new DrawLine();
        DrawStation DStation = new DrawStation();

        #region Figures
        private Ellipse _Circle = new Ellipse();
        private Line _Line = new Line();
        #endregion

        #region Commands Mouse events / Drawing
        public ICommand MouseUp { get; }
        private bool CanMouseUp(object p) => true;
        private void OnMouseUp(object p)
        {
            IsDrawing = false;

            switch (Main.Drawing)
            {
                case MainViewModel.Modes.None:
                    break;
                case MainViewModel.Modes.Station:
                {
                    DStation.Pstart = Mouse.GetPosition(p as Canvas);
                    DStation.color = Main.Color;
                    _Circle = DStation.Draw();
                    (p as Canvas).Children.Add(_Circle);
                    Main.Stationss.Add(_Circle);
                    Main.Ellipse = _Circle;

                    if (Main.VisabilityWayGrid == Visibility.Visible)
                    {
                        Main.VisabilityWayGrid = Visibility.Collapsed;
                    }
                    Main.VisabilityStationGrid = Visibility.Visible;
                    break;
                }
                case MainViewModel.Modes.Circle:
                {
                    Main.Ellipse = _Circle;
                    if (VisabilityStationGrid == Visibility.Visible)
                    {
                        Main.VisabilityStationGrid = Visibility.Collapsed;
                    }
                    Main.VisabilityWayGrid = Visibility.Visible;
                    break;
                }
                case MainViewModel.Modes.Line:
                {
                    Main.Line = _Line;
                    if (VisabilityStationGrid == Visibility.Visible)
                    {
                        Main.VisabilityStationGrid = Visibility.Collapsed;
                    }
                    Main.VisabilityWayGrid = Visibility.Visible;
                    break;
                }
            }
        }

        public ICommand MouseDown { get; }
        private bool CanMouseDown(object p) => true;
        private void OnMouseDown(object p)
        {
            IsDrawing = true;
            switch (Main.Drawing)
            {
                case MainViewModel.Modes.None:
                    break;
                //  DRAW STATION
                case MainViewModel.Modes.Station:
                    break;
                //  DRAW LINE 
                case MainViewModel.Modes.Line:
                {
                    DLine.Pstart = Mouse.GetPosition(p as Canvas);
                    DLine.Pend = Mouse.GetPosition(p as Canvas);
                    DLine.color = Main.Color;
                    _Line = DLine.Draw();
                    (p as Canvas).Children.Add(_Line);
                    Main.Liness.Add(_Line);
                    break;
                }
                //  DRAW ELLIPSE
                case MainViewModel.Modes.Circle:
                {
                    DCircle.Pstart = Mouse.GetPosition(p as Canvas);
                    DCircle.color = Main.Color;
                    DCircle.currentP = Mouse.GetPosition(p as Canvas);

                    _Circle = DCircle.Draw();
                    (p as Canvas).Children.Add(_Circle);
                    Main.Ellipses.Add(_Circle);
                    break;

                }
            }
        }

        public ICommand MouseMove { get; }
        private bool CanMouseMoved(object p) => true;
        private void OnMouseMove(object p)
        {
            if (IsDrawing == true)
                switch (Main.Drawing)
                {
                    case MainViewModel.Modes.None:
                        break;
                    case MainViewModel.Modes.Station:
                        break;
                    //  DRAW LINE 
                    case MainViewModel.Modes.Line:
                    {
                        //ViewModel   //View
                        _Line.X2 = Mouse.GetPosition(p as Canvas).X;
                        _Line.Y2 = Mouse.GetPosition(p as Canvas).Y;
                        break;
                    }
                    //  DRAW ELLIPSE
                    case MainViewModel.Modes.Circle:
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

        public ICommand AddElementCommand { get;}
        private bool CanAddElement(object p) => true;
        private void OnAddElement(object p)
        {
            Main.Children.Add(p as UIElement);
        }

        public CanvasViewModel(MainViewModel main)
        {
            Main = main;
            MouseMove = new LambdaCommand(OnMouseMove, CanMouseMoved);
            MouseDown = new LambdaCommand(OnMouseDown, CanMouseDown);
            MouseUp = new LambdaCommand(OnMouseUp, CanMouseUp);
        }
    }
}
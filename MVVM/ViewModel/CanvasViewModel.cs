using System.Windows.Controls;
using System.Windows.Input;
using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Model;
using System.Windows.Shapes;
using EditorSubwayMap.Infrastructure.Commands;
using EditorSubwayMap.MVVM.Base;
using EditorSubwayMap.Data;
using EditorSubwayMap.MVVM.Model;
using System.Windows;

namespace EditorSubwayMap.MVVM.ViewModel
{
    public class CanvasViewModel : NotifyPropertyChanged
    {
        public CanvasModel DrawingBoard = new CanvasModel();
        public MainViewModel Main;

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

        #region Propertyes
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

        public ICommand MouseUp { get; }
        private bool CanMouseUp(object p) => true;
        private void OnMouseUp(object p)
        {
            DrawingBoard.IsDrawing = false;

            switch (OnCanvas.Drawing)
            {
                case OnCanvas.Modes.None:
                    break;
                case OnCanvas.Modes.Station:
                {
                    DStation.Pstart = Mouse.GetPosition(p as Canvas);
                    DStation.color = OnCanvas.Color;
                    _Circle = DStation.Draw();
                    (p as Canvas).Children.Add(_Circle);
                    OnCanvas.Ellipse = _Circle;

                    if (Main.VisabilityWayGrid == Visibility.Visible)
                    {
                        Main.VisabilityWayGrid = Visibility.Hidden;
                    }
                    Main.VisabilityStationGrid = Visibility.Visible;
                    break;
                }
                case OnCanvas.Modes.Circle:
                {
                    OnCanvas.Ellipse = _Circle;
                    if (VisabilityStationGrid!= Visibility.Visible)
                    {
                        Main.VisabilityStationGrid = Visibility.Hidden;
                    }
                    Main.VisabilityWayGrid = Visibility.Visible;
                    break;
                }
                case OnCanvas.Modes.Line:
                {
                    OnCanvas.Line = _Line;
                    if (VisabilityStationGrid != Visibility.Visible)
                    {
                        Main.VisabilityStationGrid = Visibility.Hidden;
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
            DrawingBoard.IsDrawing = true;
            switch (OnCanvas.Drawing)
            {
                case OnCanvas.Modes.None:
                    break;
                //  DRAW STATION
                case OnCanvas.Modes.Station:
                    break;
                //  DRAW LINE 
                case OnCanvas.Modes.Line:
                {
                    DLine.Pstart = Mouse.GetPosition(p as Canvas);
                    DLine.Pend = Mouse.GetPosition(p as Canvas);
                    DLine.color = OnCanvas.Color;
                    _Line = DLine.Draw();
                    (p as Canvas).Children.Add(_Line);
                    break;

                }
                //  DRAW ELLIPSE
                case OnCanvas.Modes.Circle:
                {
                    DCircle.Pstart = Mouse.GetPosition(p as Canvas);
                    DCircle.color = OnCanvas.Color;
                    DCircle.currentP = Mouse.GetPosition(p as Canvas);

                    _Circle = DCircle.Draw();
                    (p as Canvas).Children.Add(_Circle);
                    break;

                }
            }
        }

        public ICommand MouseMove { get; }
        private bool CanMouseMoved(object p) => true;
        private void OnMouseMove(object p)
        {
            if (DrawingBoard.IsDrawing == true)
                switch (OnCanvas.Drawing)
                {
                    case OnCanvas.Modes.None:
                        break;
                    case OnCanvas.Modes.Station:
                        break;
                    //  DRAW LINE 
                    case OnCanvas.Modes.Line:
                    {
                        //ViewModel   //View
                        _Line.X2 = Mouse.GetPosition(p as Canvas).X;
                        _Line.Y2 = Mouse.GetPosition(p as Canvas).Y;
                        break;
                    }
                    //  DRAW ELLIPSE
                    case OnCanvas.Modes.Circle:
                    {
                        DCircle.currentP = Mouse.GetPosition(p as Canvas);
                        _Circle = DCircle.EditSize(_Circle);
                        break;
                    }
                }
            PositionX = "X: " + Mouse.GetPosition(p as Canvas).X;
            PositionY = "Y: " + Mouse.GetPosition(p as Canvas).Y;
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
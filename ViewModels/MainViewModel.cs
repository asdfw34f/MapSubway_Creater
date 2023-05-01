using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Infastructure.Commands;
using EditorSubwayMap.Infrastructure;
using EditorSubwayMap.Model;
using EditorSubwayMap.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EditorSubwayMap.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        #region  X Y labels
        
        private string _labelX = "0";
        public string labelX
        {
            get => _labelX;
            set => Set(ref _labelX, value);
        }

        private string _labelY = "0";
        public string labelY
        {
            get => _labelY;
            set => Set(ref _labelY, value);
        }

        #endregion

        #region Canvas Property

        private List<UIElement> _Children;
        public List<UIElement> Children
        {
            get => _Children;
            set => Set(ref _Children, value);
        }

        private Canvas _Canvas = new Canvas();
        public Canvas Canvas
        {
            get => _Canvas;
            set => Set(ref _Canvas, value);
        }

        private bool _isDrawing;
        public bool isDrawing
        {
            get => _isDrawing;
            set => Set(ref _isDrawing, value);
        }

        private string _DrawMode;
        public string DrawMode
        {
            get => _DrawMode;
            set
            {
                if (value == "Line" || value == "Circle" || value == "Station" || value == "")
                {
                    Set(ref _DrawMode, value);
                }
            }
        }

        private string _Color;
        public string Color
        {
            get => _Color;
            set => Set(ref _Color, value);
        }

        #endregion

        #region Add come station

        private string _NameStation;
        public string NameStation
        {
            get => _NameStation;
            set => Set(ref _NameStation, value);
        }

        private string _distanceNext = "0";
        public string distanceNext
        {
            get => _distanceNext;
            set => Set(ref _distanceNext, value);
        }

        private string _distanceBack = "0";
        public string distanceBack
        {
            get => _distanceBack;
            set => Set(ref _distanceBack, value);
        }

        #endregion

        #region Add come way
        
        private string _NameWay;
        public string NameWay
        {
            get => _NameWay;
            set => Set(ref _NameWay, value);
        }

        #endregion

        DrawEllipse drawCircle = new DrawEllipse();
        DrawLine drawLine = new DrawLine();
        DrawStation drawStation = new DrawStation();

        Ellipse Circle;
        Line Line;
        Ellipse Station;

        BrushConverter convColors;

        #region Select the drawing mode 

        public ICommand SelectDrawLineCommand{ get; }
        private bool CanSelectDrawLineCommand(object p) => true;
        private void OnSelectDrawLineCommand(object p) => DrawMode = "Line";

        public ICommand SelectDrawCircleCommand { get; }
        private bool CanSelectDrawCircleCommand(object p) => true;
        private void OnSelectDrawCircleCommand(object p) => DrawMode = "Circle";

        public ICommand SelectDrawStationCommand { get; }
        private bool CanSelectDrawStationCommand(object p) => true;
        private void OnSelectDrawStationCommand(object p) => DrawMode = "Station";

        public ICommand SelectDrawNoneCommand { get; }
        private bool CanSelectDrawNoneCommand(object p) => true;
        private void OnSelectDrawNoneCommand(object p) => DrawMode = "";

        #endregion

        public MainViewModel()
        {
            #region Select the drawing mode 
            SelectDrawLineCommand = new LambdaCommand(OnSelectDrawLineCommand, CanSelectDrawLineCommand);
            SelectDrawCircleCommand = new LambdaCommand(OnSelectDrawCircleCommand, CanSelectDrawCircleCommand);
            SelectDrawStationCommand = new LambdaCommand(OnSelectDrawStationCommand, CanSelectDrawStationCommand);
            SelectDrawNoneCommand = new LambdaCommand(OnSelectDrawNoneCommand, CanSelectDrawNoneCommand);
            #endregion
            MouseMoveCommand = new LambdaCommand(OnMouseMoveCommand, canMouseMoveCommand);

            Canvas.MouseDown += Canvas_MouseDown;
            Canvas.MouseUp += Canvas_MouseUp;
            Canvas.MouseMove += Canvas_MouseMove;

        }

        private ICommand _mouseClick;
        public ICommand MouseClick
        {
            get
            {
                return _mouseClick ?? (_mouseClick = new RelayCommand<object>(
                          x => { DoStuffWhenMouseClicked(); }));
            }
        }

        private static void DoStuffWhenMouseClicked()
        {
            MessageBox.Show("Mouse click event handled!");
        }

        public void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
            if (DrawMode == "")
                return;
            else if (DrawMode == "Station")
            {
                drawStation.Pstart = e.GetPosition(Canvas);
                drawStation.color = convColors.ConvertFromString(Color) as Brush;
                Circle = drawStation.Draw();
                Children.Add(Circle);
            }
        }

        public void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && isDrawing == true)
                switch (DrawMode)
                {
                    //  DRAW LINE 
                    case "Line":
                        Line.X2 = e.GetPosition(Canvas).X;
                        Line.Y2 = e.GetPosition(Canvas).Y;
                        break;

                    //  DRAW ELLIPSE
                    case "Circle":
                        drawCircle.currentP = e.GetPosition(Canvas);
                        Circle = drawCircle.EditSize(Circle);
                        break;
                }

            labelX = "X: " + e.GetPosition(Canvas).X;
            labelY = "Y: " + e.GetPosition(Canvas).Y;
        }

        public void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            switch (DrawMode)
            {
                case "":
                    break;

                //  DRAW LINE 
                case "Line":
                    NameWay = "Название ветки: ";
                    drawLine.Pstart = e.GetPosition(Canvas);
                    drawLine.Pend = e.GetPosition(Canvas);
                    drawLine.color = convColors.ConvertFromString(Color) as Brush;

                    Line = drawLine.Draw();
                    Children.Add(Line);
                    break;

                //  DRAW STATION
                case "Station":
                    break;

                //  DRAW ELLIPSE
                case "Circle":
                    NameWay = "Название ветки: ";
                    drawCircle.Pstart = e.GetPosition(Canvas);
                    drawCircle.color = convColors.ConvertFromString(Color) as Brush;
                    drawCircle.currentP = e.GetPosition(Canvas);

                    Circle = drawCircle.Draw();
                    Children.Add(Circle);
                    break;
            }
        }

        public ICommand MouseMoveCommand { get; }
        private bool canMouseMoveCommand(object p) => true;
        private void OnMouseMoveCommand(object p)
        {
            var e = p as MouseButtonEventArgs;
            if (e.LeftButton == MouseButtonState.Pressed && isDrawing == true)
                switch (DrawMode)
                {
                    //  DRAW LINE 
                    case "Line":
                        Line.X2 = e.GetPosition(Canvas).X;
                        Line.Y2 = e.GetPosition(Canvas).Y;
                        break;

                    //  DRAW ELLIPSE
                    case "Circle":
                        drawCircle.currentP = e.GetPosition(Canvas);
                        Circle = drawCircle.EditSize(Circle);
                        break;
                }

            labelX = "X: " + e.GetPosition(Canvas).X;
            labelY = "Y: " + e.GetPosition(Canvas).Y;
        }


    }
}
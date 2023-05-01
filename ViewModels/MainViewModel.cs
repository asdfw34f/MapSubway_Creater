using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Infastructure.Commands;
using EditorSubwayMap.Model;
using EditorSubwayMap.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private string _labelX;
        public string labelX
        {
            get => _labelX;
            set => Set(ref _labelX, value);
        }

        private string _labelY;
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

        private Canvas _Canvas;
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

        private string _distanceNext;
        public string distanceNext
        {
            get => _distanceNext;
            set => Set(ref _distanceNext, value);
        }

        private string _distanceBack;
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

        #region Canvas mouse events

        DrawEllipse drawCircle = new DrawEllipse();
        DrawLine drawLine = new DrawLine();
        DrawStation drawStation = new DrawStation();

        Ellipse Circle;
        Line Line;
        Ellipse Station;

        BrushConverter convColors;

        public ICommand CanvasMouseMove { get; }
        private bool CanCanvasMouseMove(object p) => true;
        private void OnCanvasMouseMove(object p)
        {
            var e = (p as MouseButtonEventArgs);
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

        public ICommand CanvasMouseUp { get; }
        private bool CanCanvasMouseUp(object p) => true;
        private void OnCanvasMouseUp(object p)
        {
            isDrawing = false;
            if (DrawMode == "")
                return;
            else if (DrawMode == "Station")
            {
                drawStation.Pstart = (p as MouseButtonEventArgs).GetPosition(Canvas);
                drawStation.color = convColors.ConvertFromString(Color) as Brush;
                Circle = drawStation.Draw();
                Children.Add(Circle);
            }
        }

        public ICommand CanvasMouseDown { get; }
        private bool CanCanvasMouseDown(object p) => true;
        private void OnCanvasMouseDown(object p)
        {
            var e = (p as MouseButtonEventArgs);
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

        #endregion
        public MainViewModel()
        {
            #region Canvas mouse events
            CanvasMouseUp = new LambdaCommand(OnCanvasMouseUp, CanCanvasMouseUp);
            CanvasMouseMove = new LambdaCommand(OnCanvasMouseMove, CanCanvasMouseMove);
            CanvasMouseDown = new LambdaCommand(OnCanvasMouseDown, CanCanvasMouseDown);
            #endregion
        }
    }
}
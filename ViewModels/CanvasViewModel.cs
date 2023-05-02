using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Model;
using EditorSubwayMap.Models;
using EditorSubwayMap.Infastructure.Commands;

using System;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using EditorSubwayMap.ViewModels.Base;

namespace EditorSubwayMap.ViewModels
{
    public class CanvasViewModel : ViewModel
    {
        private MainViewModel _mainvm;
        public MyCanvas DrawingBoard { get; set; }

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
        public CanvasViewModel(MainViewModel mainvm)
        {
            _mainvm = mainvm;
            DrawingBoard = new MyCanvas { canvas = new Canvas(), isDrawing = false};
            DrawingBoard.canvas.Background = new SolidColorBrush(Colors.WhiteSmoke);
            DrawingBoard.canvas.AllowDrop = true;
            DrawingBoard.canvas.Focusable = true;
            DrawingBoard.canvas.MouseDown += CanvasClicked;
            DrawingBoard.canvas.MouseMove += CanvasMove;
            DrawingBoard.canvas.MouseUp += CanvasRelease;
            DrawingBoard.canvas.PreviewMouseMove += CanvasMove;
        }

        public void DrawShape()
        { 

        }

        DrawEllipse drawCircle = new DrawEllipse();
        DrawLine drawLine = new DrawLine();
        DrawStation drawStation = new DrawStation();

        Ellipse circle;
        Line line;
        Ellipse station;

        private void CanvasMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && DrawingBoard.isDrawing == true)
                switch (DrawingBoard.DrawMode)
                {
                    //  DRAW LINE 
                    case "Line":
                        line.X2 = e.GetPosition(DrawingBoard.canvas).X;
                        line.Y2 = e.GetPosition(DrawingBoard.canvas).Y;
                        break;

                    //  DRAW ELLIPSE
                    case "Circle":
                        drawCircle.currentP = e.GetPosition(DrawingBoard.canvas);
                        circle = drawCircle.EditSize(circle);
                        break;
                }
            labelX = "X: " + e.GetPosition(DrawingBoard.canvas).X;
            labelY = "Y: " + e.GetPosition(DrawingBoard.canvas).Y;
        }

        private void CanvasRelease(object sender, RoutedEventArgs e)
        {

        }

        private void CanvasClicked(object sender, RoutedEventArgs e)
        {

        }

        private void ShapeMove(object sender, RoutedEventArgs e)
        {

        }

        private void ShapeReleased(object sender, RoutedEventArgs e)
        {

        }

        private void ClickShape(object sender, RoutedEventArgs e)
        {

        }

        public void ChangeShapeColor(string color)
        {

        }

        public void ChangeShangeSize(string op)
        {

        }

        public void CreateShape(string shape)
        {

        }

        public void RemoveShape()
        {

        }
    }
}
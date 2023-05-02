using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Model;
using EditorSubwayMap.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using EditorSubwayMap.ViewModels.Base;

namespace EditorSubwayMap.ViewModels
{
    public class CanvasViewModel : ViewModel
    {
        public MyCanvas DrawingBoard
        { get;
            set; }
        private MainViewModel _mainvm;

        public CanvasViewModel(MainViewModel mainvm)
        {
            this._mainvm = mainvm;
            DrawingBoard = new MyCanvas
            {
                DrawingCanvas = new Canvas(),
                IsDrawing = false
            };
            DrawingBoard.DrawingCanvas.Background = new SolidColorBrush(Colors.WhiteSmoke);
            DrawingBoard.DrawingCanvas.AllowDrop = true;
            DrawingBoard.DrawingCanvas.Focusable = true;
            DrawingBoard.DrawingCanvas.MouseLeftButtonDown += CanvasDown;
            DrawingBoard.DrawingCanvas.MouseMove += CanvasMove;
            DrawingBoard.DrawingCanvas.MouseLeftButtonUp += CanvasRelease;
            DrawingBoard.DrawingCanvas.PreviewMouseMove += CanvasMove;
        }

        public void DrawShape()
        { 

        }

        DrawEllipse drawCircle = new DrawEllipse();
        DrawLine drawLine = new DrawLine();
        DrawStation drawStation = new DrawStation();
//     <ContentPresenter Content="{Binding Path=DrawingBoard.DrawingCanvas}"/>

        Ellipse circle;
        Line line;
        Ellipse station;

        private void CanvasMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && DrawingBoard.IsDrawing == true)
                switch (DrawingBoard.DrawMode)
                {
                    //  DRAW LINE 
                    case "Line":
                        line.X2 = e.GetPosition(DrawingBoard.DrawingCanvas).X;
                        line.Y2 = e.GetPosition(DrawingBoard.DrawingCanvas).Y;
                        break;

                    //  DRAW ELLIPSE
                    case "Circle":
                        drawCircle.currentP = e.GetPosition(DrawingBoard.DrawingCanvas);
                        circle = drawCircle.EditSize(circle);
                        break;
                }
            _mainvm.labelX = "X: " + e.GetPosition(DrawingBoard.DrawingCanvas).X;
            _mainvm.labelY = "Y: " + e.GetPosition(DrawingBoard.DrawingCanvas).Y;
        }

        private void CanvasRelease(object sender, MouseEventArgs e)
        {

        }

        private void CanvasDown(object sender, MouseEventArgs e)
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
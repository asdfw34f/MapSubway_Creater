using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Model;
using EditorSubwayMap.Models;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using EditorSubwayMap.Infastructure.Commands;
using EditorSubwayMap.ViewModels.Base;
using WpfApp1.Data;

namespace EditorSubwayMap.ViewModels
{
    public class CanvasViewModel : ViewModel
    {
        public MyCanvas DrawingBoard { get; set; }

        public CanvasViewModel()
        {
            DrawingBoard = new MyCanvas
            {
                IsDrawing = false
            };
            MouseMove = new LambdaCommand(OnMouseMove, CanMouseMoved);
            MouseDown = new LambdaCommand(OnMouseDown, CanMouseDown);
            MouseUp = new LambdaCommand(OnMouseUp, CanMouseUp);
        }

        DrawEllipse DCircle = new DrawEllipse();
        DrawLine DLine = new DrawLine();
        DrawStation DStation = new DrawStation();

        Ellipse Circle;
        Line Line;
        Ellipse Station;
        #region MouseEvents commands
        public ICommand MouseDown { get; }
        private bool CanMouseDown(object p) => true;

        private void OnMouseDown(object p)
        {
            DrawingBoard.IsDrawing = true;
            switch (DrawingOnCanvas.Drawing)
            {
                case DrawingOnCanvas.Modes.None:
                    break;

                //  DRAW LINE 
                case  DrawingOnCanvas.Modes.Line:
                    //NameWay = "Название ветки: ";
                    DLine.Pstart = Mouse.GetPosition(p as Canvas);
                    DLine.Pend = Mouse.GetPosition(p as Canvas);
                   // DLine.color = convColors.ConvertFromString(Color) as Brush;

                    Line = DLine.Draw();
                    DrawingBoard.Children.Add(Line);
                    break;

                //  DRAW STATION
                case  DrawingOnCanvas.Modes.Station:
                    break;

                //  DRAW ELLIPSE
                case  DrawingOnCanvas.Modes.Circle:
                    //NameWay = "Название ветки: ";
                    DCircle.Pstart = Mouse.GetPosition(p as Canvas);
              //      DCircle.color = convColors.ConvertFromString(Color) as Brush;
                    DCircle.currentP = Mouse.GetPosition(p as Canvas);

                    Circle = DCircle.Draw();
                    DrawingBoard.Children.Add(Circle);
                    break;
            }
        }

        public ICommand MouseUp { get; }
        private bool CanMouseUp(object p) => true;

        private void OnMouseUp(object p)
        {
            DrawingBoard.IsDrawing = false;
            if (DrawingOnCanvas.Drawing == DrawingOnCanvas.Modes.None)
                return;
            else if (DrawingOnCanvas.Drawing == DrawingOnCanvas.Modes.Station)
            {
                DStation.Pstart = Mouse.GetPosition(p as Canvas);
                //DStation.color = convColors.ConvertFromString(Color) as Brush;
                Circle = DStation.Draw();
                DrawingBoard.Children.Add(Circle);
            }
        }

        public ICommand MouseMove { get; }
        private bool CanMouseMoved(object p) => true;

        private void OnMouseMove(object p)
        {
            switch (DrawingOnCanvas.Drawing)
            {
                //  DRAW LINE 
                case DrawingOnCanvas.Modes.Line:
                    //ViewModel   //View
                    //         line.X2 = e.GetPosition(DrawingBoard.DrawingCanvas).X;
                    //         line.Y2 = e.GetPosition(DrawingBoard.DrawingCanvas).Y;
                    break;

                //  DRAW ELLIPSE
                case DrawingOnCanvas.Modes.Circle:
                    //                drawCircle.currentP = e.GetPosition(DrawingBoard.DrawingCanvas);
                    Circle = DCircle.EditSize(Circle);
                    break;
            }
        }

        #endregion
    }
}

using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Model;
using WpfApp1.Data;

namespace EditorSubwayMap.Infastructure.Commands.CanvasMouseEvents
{
    public class CanvasEventsCommands
    {
        DrawEllipse DCircle = new DrawEllipse();
        DrawLine DLine = new DrawLine();
        DrawStation DStation = new DrawStation();

        #region Figures
            private Ellipse _Circle;
            private Line _Line;
            private Ellipse _Station;
        #endregion
        
        public ICommand MouseUp { get; }
        private bool CanMouseUp(object p) => true;
        private void OnMouseUp(object p)
        {
            DrawingOnCanvas.DrawingBoard.IsDrawing = false;

            if (DrawingOnCanvas.Drawing == DrawingOnCanvas.Modes.None)
                return;
            else if (DrawingOnCanvas.Drawing == DrawingOnCanvas.Modes.Station)
            {
                DStation.Pstart = Mouse.GetPosition(p as Canvas);
                //DStation.color = convColors.ConvertFromString(Color) as Brush;
                _Circle = DStation.Draw();
                DrawingOnCanvas.DrawingBoard.Children.Add(_Circle);
            }
        }
        
        public ICommand MouseDown { get; }
        private bool CanMouseDown(object p) => true;
        private void OnMouseDown(object p)
        {
            DrawingOnCanvas.DrawingBoard.IsDrawing = true;
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

                    _Line = DLine.Draw();
                    DrawingOnCanvas.DrawingBoard.Children.Add(_Line);
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

                    _Circle = DCircle.Draw();
                    DrawingOnCanvas.DrawingBoard.Children.Add(_Circle);
                    break;
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
                    _Circle = DCircle.EditSize(_Circle);
                    break;
            }

            DrawingOnCanvas.PositionX = Mouse.GetPosition(p as Canvas).X.ToString();
            DrawingOnCanvas.PositionY = Mouse.GetPosition(p as Canvas).Y.ToString();
        }

        internal CanvasEventsCommands()
        {
            MouseMove = new LambdaCommand(OnMouseMove, CanMouseMoved);
            MouseDown = new LambdaCommand(OnMouseDown, CanMouseDown);
            MouseUp = new LambdaCommand(OnMouseUp, CanMouseUp);
        }
    }
}
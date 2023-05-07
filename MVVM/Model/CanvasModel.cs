using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using EditorSubwayMap.MVVM.Base;

namespace EditorSubwayMap.MVVM.Model
{
    public class CanvasModel : NotifyPropertyChanged
    {
        #region Fields
        private UIElement _Children = new UIElement();
        private Point _StartPoint = new Point();
        private Point _EndPoint = new Point();
        private Ellipse _Ellipse = new Ellipse();
        private Line _Line = new Line();
        private bool _IsDrawing;
        #endregion

        public Line Line
        {
            get => _Line; 
            set => Set(ref _Line, value);
        }

        public Ellipse Ellipse
        {
            get => _Ellipse; 
            set => Set(ref _Ellipse, value);
        }
       
        private Canvas _canvas = new Canvas() { Background=Brushes.Black};
        public Canvas Canvas
        {
            get => _canvas;
            set => Set(ref _canvas, value);
        }

        public bool IsDrawing
        {
            get => _IsDrawing;
            set=> Set(ref _IsDrawing, value);
        }

        public Point StartPoint
        {
            get => _StartPoint;
            set => Set(ref _StartPoint, value);
        }

        public Point EndPoint
        {
            get => _EndPoint; 
            set => Set(ref _EndPoint, value);
        }
    }
}
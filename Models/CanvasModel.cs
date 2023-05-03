using System.Collections.Generic;
using System.Windows;
using System.Windows.Shapes;

namespace EditorSubwayMap.Models
{
    public class CanvasModel : Base.Model
    {
        #region Fields
        private List<UIElement> _Children = new List<UIElement>();
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
       
        public List<UIElement> Children
        {
            get => _Children;
            set => Set(ref _Children, value);
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
using System.Collections.Generic;
using System.Windows;

namespace EditorSubwayMap.Models
{
    public class CanvasModel
    {
    }
    public class MyCanvas
    {
        public Point StartPoint { get; set; }
        public List<UIElement> Children { get; set; }
        public bool IsDrawing { get; set; }
    }
}
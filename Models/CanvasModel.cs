using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace EditorSubwayMap.Models
{
    public class CanvasModel
    {
    }
    public class MyCanvas
    {
        public string DrawMode { get; set; }

        public Point StartPoint { get; set; }
        
        public bool IsDrawing { get; set; }
    }
}
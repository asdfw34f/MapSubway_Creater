using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EditorSubwayMap.Data
{
    public static class OnCanvas
    {
        public enum Modes
        {
            None,
            Circle,
            Line,
            Station
        }

        public static Visibility VisabilityStationGrid;
        public static Visibility VisabilityWayGrid;

        public static Ellipse Ellipse { get; set; }
        public static Line Line { get; set; }
        public static Brush Color { get; set; } = Brushes.Black;
        public static string PositionX { get; set; }
        public static string PositionY { get; set; }
    }
}
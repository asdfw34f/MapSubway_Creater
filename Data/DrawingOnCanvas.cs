using System.ComponentModel;

namespace WpfApp1.Data
{
    public static class DrawingOnCanvas
    {
        public enum Modes
        {
            None,
            Circle,
            Line,
            Station
        }

        public static Modes Drawing { get; set; }
        public static string Color { get; set; }
    }
}
// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Windows.Media;
using System.Windows;

namespace DrawMapMetroLibrary.Atributs
{
    public class EllipseWay
    {
        public string NameWay { get; set; } = "Undefined";
        public Point Position { get; set; } = new Point(1, 1);
        public Brush Color { get; set; } = Brushes.Black;
        public double Height { get; set; } = 20;
        public double Width { get; set; } = 20;

        public EllipseWay()
        {
        }
    }
}

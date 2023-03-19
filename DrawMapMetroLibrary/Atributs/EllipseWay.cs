// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Windows.Media;
using System.Windows;

namespace DrawMapMetroLibrary.Atributs
{
    internal class EllipseWay
    {
        internal string NameWay { get; set; } = "Undefined";
        internal Point Position { get; set; } = new Point(1, 1);
        internal Brush Color { get; set; } = Brushes.Black;
        double Height { get; set; } = 20;
        double Width { get; set; } = 20;

        internal EllipseWay(string NameWay, Point Position, 
            Brush brush, double Height, double Width)
        {
            this.NameWay = NameWay;
            this.Position = Position;
            Color = brush;
            this.Height = Height;
            this.Width = Width;
        }
    }
}

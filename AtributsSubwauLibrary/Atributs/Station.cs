// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Windows;

namespace DrawMapMetroLibrary.Atributs
{
    public class Station
    {
        public string NameStation { get; set; } = "Undefined";
        public double NextWay { get; set; } = 1;
        public double BackWay { get; set; } = 1;
        public string NameWay { get; set; } = "Undefined";
        public string Color { get; set; } = "#FF000000";
        public Point Position { get; set; } = new Point(1, 1);

        public Station()
        {
        }
    }
}

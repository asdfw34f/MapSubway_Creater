// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Windows;
using System.Windows.Media;

namespace DrawMapMetroLibrary.Atributs
{
    public class Station
    {
        internal string NameStation { get; set; } = "Undefined";
        internal int NextWay { get; set; } = 1;
        internal int BackWay { get; set; } = 1;
        internal string NameWay { get; set; } = "Undefined";
        internal string Color { get; set; } = "#FF000000";
        internal Point Position { get; set; } = new Point(1, 1);

        public Station()
        {
        }
    }
}

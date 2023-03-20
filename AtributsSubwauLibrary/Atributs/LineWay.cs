// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Windows;
using System.Windows.Media;

namespace DrawMapMetroLibrary.Atributs
{
    public class LineWay
    {
        internal string NameWay { get; set; } = "Undefined";
        internal Point Start { get; set; } = new Point(1, 1);
        internal Point End { get; set; } = new Point(1, 1);
        internal string Color { get; set; } = "#FF000000";

        public LineWay()
        {
        }
    }
}

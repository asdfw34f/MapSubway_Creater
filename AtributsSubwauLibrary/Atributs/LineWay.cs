// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Windows;

namespace DrawMapMetroLibrary.Atributs
{
    [Serializable()]
    public class LineWay
    {
        public string WayID { get; set; } = "A";
        public string NameWay { get; set; } = "Undefined";
        public Point Start { get; set; } = new Point(1, 1);
        public Point End { get; set; } = new Point(1, 1);
        public string Color { get; set; } = "#FF000000";

        public LineWay()
        {
        }
    }
}
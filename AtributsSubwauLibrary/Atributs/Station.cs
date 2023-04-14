// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Windows;

namespace DrawMapMetroLibrary.Atributs
{
    [Serializable()]
    public class Station
    {
        public string StationID { get; set; } = "A";
        public string NameStation { get; set; } = "Undefined";
        public string NameWay { get; set; } = "Undefined";
        public int Back { get; set; } = 0;
        public int Go { get; set; } = 0;
        public string Color { get; set; } = "#FF000000";
        public Point Position { get; set; } = new Point(1, 1);

        public Station()
        {
        }
    }
}
// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawMapMetroLibrary.Atributs
{
    internal class Station
    {
        internal string NameStation { get; set; } = "Undefined";
        internal int NextWay { get; set; } = 1;
        internal int BackWay { get; set; } = 1;
        internal string NameWay { get; set; } = "Undefined";
        internal Brush Color { get; set; } = Brushes.Black;
        internal Point Position { get; set; } = new Point(1, 1);

        internal Station(string nameStation, int nextWay, 
            int backWay, string NameWay, Brush brush, Point point)
        {
            NameStation = nameStation;
            NextWay = nextWay;
            BackWay = backWay;
            this.NameWay = NameWay;
            Color = brush;
            Position = point;
        }
    }
}

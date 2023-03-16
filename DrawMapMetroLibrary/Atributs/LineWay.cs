// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawMapMetroLibrary.Atributs
{
    internal class LineWay
    {
        internal string NameWay { get; set; } = "Undefined";
        internal Point Start { get; set; } = new Point(1, 1);
        internal Point End { get; set; } = new Point(1, 1);
        internal Brush Color { get; set; } = Brushes.Black;

        internal LineWay(string NameWay, Point Start, Point End, Brush brush)
        {
            this.NameWay = NameWay;
            this.Start = Start;
            this.End = End;
            Color = brush;
        }
    }
}

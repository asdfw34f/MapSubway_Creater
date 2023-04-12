// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Xml.Serialization;
using System.Windows.Shapes;
using System;
using System.Windows;

namespace DrawMapMetroLibrary.Atributs
{
    [Serializable()]
    public class EllipseWay
    {
        public string NameWay { get; set; } = "Undefined";
        public Point Position { get; set; } = new Point(1, 1);
        public string Color { get; set; } = "#FF000000";
        public double Height { get; set; } = 20;
        public double Width { get; set; } = 20;

        public EllipseWay()
        {
        }
    }
}
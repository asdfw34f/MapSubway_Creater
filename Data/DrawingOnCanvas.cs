﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using EditorSubwayMap.Models;

namespace WpfApp1.Data
{
    public static class DrawingOnCanvas
    {
        public enum Modes
        {
            None,
            Circle,
            Line,
            Station
        }

        public static Modes Drawing { get; set; }
        public static Ellipse Ellipse { get; set; }
        public static Line Line { get; set; }
        public static string PositionX { get; set; }
        public static string PositionY { get; set; }
        public static Brush Color { get; set; }
        public static List<UIElement> Children { get; set; }
    }
}
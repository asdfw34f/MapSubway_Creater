using System.Collections.Generic;
using System.Windows;
using EditorSubwayMap.Models;

namespace EditorSubwayMap.Data.ElementsOfMap
{
    public class CircleWay
    {
        public string Name { get; set; }
        public List<Station> stations { get; set; }
        public string Color { get; set; }
        public Point Position { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public CircleWay() { }
    }
}
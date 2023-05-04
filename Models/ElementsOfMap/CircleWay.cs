using System.Collections.Generic;
using System.Windows;

namespace EditorSubwayMap.Models.ElementsOfMap
{
    public class CircleWay
    {
        public string Name { get; set; }
        public List<Station> stations { get; set; }
        public string Color { get; set; }
        public Point Position { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
    }
}
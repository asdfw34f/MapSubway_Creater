using System.Collections.Generic;
using System.Windows;

namespace EditorSubwayMap.Models.ElementsOfMap
{
    public class LineWay
    {
        public string Name { get; set; }
        public List<Station> stations { get; set; }
        public string Color { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
    }
}
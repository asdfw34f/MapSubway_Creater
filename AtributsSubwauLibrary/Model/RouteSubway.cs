using System.Collections.Generic;
using System.Windows;

namespace AtributsSubwayLibrary.Model
{
    public class RouteSubway
    {
        public List<Station> stations { get; set; } = new List<Station>();
        public List<LineWay> lineWays { get; set; } = new List<LineWay>();
        public List<CircleWay> circleWays { get; set; } = new List<CircleWay>();

        public RouteSubway() { }
    }

    public class Station
    {
        public string StationID { get; set; }
        public string Name { get; set; }
        public int distanceLast { get; set; }
        public int distanceBack { get; set; }
        public string NameWay { get; set; }
        public string Color { get; set; }
        public Point Position { get; set; }
        public Station() { }
    }

    public class LineWay
    {
        public string NameWay { get; set; }
        public string Color { get; set; }
        public Point startPoint { get; set; }
        public Point endPoint { get; set; }
        public List<Station> stations { get; set; } = new List<Station>();
        public LineWay() { }
    }

    public class CircleWay
    {
        public string NameWay { get; set; }
        public string Color { get; set; }
        public Point Position { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public List<Station> stations { get; set; } = new List<Station>();
        public CircleWay() { }
    }
}
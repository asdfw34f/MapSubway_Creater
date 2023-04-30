using System.Windows;

namespace EditorSubwayMap.Models
{
    public class Station
    {
        public string Name { get; set; }
        public int StationId { get; set; }
        public int DistanceLast { get; set; }
        public int DistanceBack { get; set; }
        public Point Position { get; set; }
        public Station() { }
    }
}
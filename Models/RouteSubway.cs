using System.Collections.Generic;
using EditorSubwayMap.Models.ElementsOfMap;

namespace EditorSubwayMap.Models
{
    public class RouteSubway
    {
        public List<CircleWay> CircleWays { get; set; } = new List<CircleWay>();
        public List<LineWay> LineWays { get; set; } = new List<LineWay>();
        public List<Station> Stations { get; set; } = new List<Station>();
    }
}
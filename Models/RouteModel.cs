using AtributsSubwauLibrary.Atributs;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace EditorSubwayMap.Models
{
    internal class RouteModel
    {
        public class Route
        {
            MapSubway subway;

            public Route() { }

            public bool Save(string folder, MapSubway subway)
            {
                this.subway = subway;
                try
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(MapSubway));
                    using (FileStream fs = new FileStream(folder + "\\MapMetro.xml", FileMode.Create))
                    {
                        formatter.Serialize(fs, this.subway);
                    }
                    return true;
                }
                catch (XmlException ex)
                {
                    MessageBox.Show(ex.Message, ex.Source);
                    return false;
                }
            }

            public bool Import(string folder)
            {
                List<Line> lines = new List<Line>();
                List<Ellipse> ellipses = new List<Ellipse>();
                List<Ellipse> stations = new List<Ellipse>();
                MapSubway map = new MapSubway();

                using (FileStream file = new FileStream(folder + "\\MapMetro.xml", FileMode.Open))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(MapSubway));
                    map = formatter.Deserialize(file) as MapSubway;

                    if (map != null)
                    {
                        lines = DrawingLine(map);
                        ellipses = DrawingEllipse(map.);
                        stations = DrawingStation(map.stations);
                    }
                    else
                        goto exit;
                }

                if (!CheckWay())
                {
                    MessageBox.Show(
                        "Одна или несколько станций не имеют привязки " +
                        "к линии метро или такой линии не существует",
                        "Ошибка при импорте схемы",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return null;
                }
exit:
                return map;
            }

        }

        public class MapSubway
        {
            List<Station> stations { get; set; }
            List<LineWay> lineways { get; set; }
            List<CircleWay> circleWays { get; set; }
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
            public LineWay() { }
        }

        public class CircleWay
        {
            public string NameWay{ get; set;}
            public string Color { get; set; }
            public Point Position { get; set; }
            public double Height { get; set; }
            public double Width { get; set; }
            public CircleWay() { }
        }
    }
}
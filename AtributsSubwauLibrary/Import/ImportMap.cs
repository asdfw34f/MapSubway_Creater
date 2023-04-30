using AtributsSubwauLibrary.Model;
using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Model;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace AtributsSubwauLibrary.Import
{
    public class ImportMap
    {
        List<Line> lines = new List<Line>();
        List<Ellipse> ellipses = new List<Ellipse>();
        List<Ellipse> stations = new List<Ellipse>();
        RouteSubway map;
        
        public ImportMap() { }

        public bool CheckWay()
        {
            bool isTrue = false;
            foreach (Station st in map.stations)
            {
                foreach (CircleWay circleways in map.circleWays)
                {
                    if (st.NameWay == circleways.NameWay)
                    {
                        isTrue = true;
                        break;
                    }
                    else
                        continue;
                }
                if (!isTrue)
                {
                    foreach (LineWay lineways in map.lineWays)
                    {
                        if (st.NameWay == lineways.NameWay)
                        {
                            isTrue = true;
                        }
                    }
                    if (!isTrue)
                    {
                        return isTrue;
                    }
                }
            }
            return isTrue;
        }

        public RouteSubway Import(string Folder)
        {
            using (FileStream file = new FileStream(Folder + "\\MapMetro.xml", FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(RouteSubway));
                map = formatter.Deserialize(file) as RouteSubway;

                if (map != null)
                {
                    lines = DrawingLine(map.lineWays);
                    ellipses = DrawingEllipse(map.circleWays);
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

        public List<Ellipse> DrawingStation(List<Station> sts)
        {
            List<Ellipse> els = new List<Ellipse>();
            BrushConverter brush = new BrushConverter();
            foreach (Station stat in sts)
            {
                DrawStation ds = new DrawStation()
                {
                    color = brush.ConvertFromString(stat.Color.ToString()) as Brush,
                    Pstart = stat.Position,
                };
                els.Add(ds.Draw());
            }
            return els;
        }

        public List<Line> DrawingLine(List<LineWay> Ways)
        {
            List<Line> lines = new List<Line>();
            BrushConverter brush = new BrushConverter();

            foreach (LineWay way in Ways)
            {
                DrawLine dl = new DrawLine()
                {
                    Pend = way.endPoint,
                    Pstart = way.startPoint,
                    color = brush.ConvertFromString(way.Color.ToString()) as Brush,
                };
                lines.Add(dl.Draw());
            }
            return lines;
        }

        public List<Ellipse> DrawingEllipse(List<CircleWay> ways)
        {
            List<Ellipse> newWay = new List<Ellipse>();
            foreach (CircleWay way in ways)
            {
                BrushConverter _conv = new BrushConverter();
                DrawEllipse de = new DrawEllipse()
                {
                    Pstart = way.Position,
                    Height = way.Height,
                    Width = way.Width,
                    color = _conv.ConvertFromString(way.Color) as Brush,
                };
                newWay.Add(de.Draw());
            }
            return newWay;
        }
    }
}
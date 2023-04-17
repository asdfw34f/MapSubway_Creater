using AtributsSubwauLibrary.Atributs;
using DrawMapMetroLibrary.Atributs;
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
        public ImportMap() { }

        public List<Line> lines { get; set; } = new List<Line>();
        public List<Ellipse> ellipses { get; set; } = new List<Ellipse>();
        public List<Ellipse> stations { get; set; } = new List<Ellipse>();
        public AllMap map { get; set; } = new AllMap();

        private bool CheckWay()
        {
            bool isTrue = false;
            foreach (Station st in map.stations)
            {
                foreach (EllipseWay way in map.eways)
                {
                    if (st.NameWay == way.NameWay)
                    {
                        isTrue = true;
                        break;
                    }
                    else
                        continue;
                }
                if (!isTrue)
                {
                    foreach (LineWay way1 in map.lways)
                    {
                        if (st.NameWay == way1.NameWay)
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

        public AllMap Import(string Folder)
        {
            using (FileStream file = new FileStream(Folder + "\\MapMetro.xml", FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(AllMap));
                map = formatter.Deserialize(file) as AllMap;

                if (map != null)
                {
                    lines = DrawingLine(map.lways);
                    ellipses = DrawingEllipse(map.eways);
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
                    Pend = way.End,
                    Pstart = way.Start,
                    color = brush.ConvertFromString(way.Color.ToString()) as Brush,
                };

                lines.Add(dl.Draw());
            }
            return lines;
        }

        private List<Ellipse> DrawingEllipse(List<EllipseWay> ways)
        {
            List<Ellipse> newWay = new List<Ellipse>();
            foreach (EllipseWay way in ways)
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
using DrawMapMetroLibrary.Atributs;
using EditorSubwayMap.DrawFigure;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace AtributsSubwauLibrary.Import
{
    public class ImportEllipseWay
    {
        Canvas canvas { get; set; } = null;

        public ImportEllipseWay(Canvas can)
        {
            canvas = can;
        }

        private List<EllipseWay> Deserialize(Stream stream)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<EllipseWay>));
            List<EllipseWay> ways = formatter.Deserialize(stream) as List<EllipseWay>;

            if (ways != null)
            {
                foreach (EllipseWay way in ways)
                {
                    ways.Add(way);
                }
                return ways;
            }
            return null;
        }

        private List<Ellipse> Drawing(List<EllipseWay> ways)
        {
            List<Ellipse> newWay = new List<Ellipse>();
            foreach (EllipseWay way in ways)
            {
                BrushConverter _conv = new BrushConverter();
                DrawEllipse de = new DrawEllipse(canvas)
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

        public List<Ellipse> Import(string Folder)
        {
            List<EllipseWay> ways = new List<EllipseWay>();

            using (FileStream file = new FileStream(Folder + "\\LineWays.xml", FileMode.Open))
            {
                ways = Deserialize(file);
                if (ways != null)
                {
                    List<Ellipse> way = Drawing(ways);
                    return way;
                }
            }
            return null;
        }
    }
}
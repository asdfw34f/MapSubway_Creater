using DrawMapMetroLibrary.Atributs;
using EditorSubwayMap.Model;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace AtributsSubwauLibrary.Import
{
    public class ImportLineWay
    {
        Canvas canvas { get; set; } = null;

        public ImportLineWay(Canvas can)
        {
            canvas = can;
        }

        private List<LineWay> Deserialize(Stream stream)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<LineWay>));
            List<LineWay> ways = formatter.Deserialize(stream) as List<LineWay>;

            if (ways != null)
            {
                foreach (LineWay way in ways)
                {
                    ways.Add(way);
                }
                return ways;
            }
            return null;
        }

        private List<Line> Drawing(List<LineWay> ways)
        {
            List<Line> newWay = new List<Line>();
            foreach (LineWay way in ways)
            {
                BrushConverter _conv = new BrushConverter();
                DrawLine dl = new DrawLine(canvas)
                {
                    Pstart = way.Start,
                    Pend= way.End,
                    color = _conv.ConvertFromString(way.Color) as Brush,
                };
                newWay.Add(dl.Draw());
            }
            return newWay;
        }

        public List<Line> Import(string Folder)
        {
            List<LineWay> ways = new List<LineWay>();

            using (FileStream file = new FileStream(Folder + "\\LineWays.xml", FileMode.Open))
            {
                ways = Deserialize(file);
                if (ways != null)
                {
                    List<Line> way = Drawing(ways);
                    return way;
                }
            }
            return null;
        }
    }
}
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
        List<LineWay> lWays = new List<LineWay>();
        Canvas canvas= null;

        public ImportLineWay(Canvas canvas)
        {
            this.canvas = canvas;
        }

        private List<LineWay> Deserialize(Stream stream)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<LineWay>));
            List<LineWay> ways = formatter.Deserialize(stream) as List<LineWay>;
            return ways;
        }

        public List<Line> Drawing(List<LineWay> Ways)
        {
            List<Line> lines = new List<Line>();
            BrushConverter brush= new BrushConverter();

            foreach (LineWay way in Ways)
            {
                DrawLine dl = new DrawLine()
                {
                    Pend = way.End,
                    Pstart = way.Start,
                    color = brush.ConvertFromString(way.Color.ToString()) as Brush,
                    iditLoc = false
                };
                
                lines.Add(dl.Draw());
            }
            return lines;
        }

        public List<Line> Import(string Folder)
        {
            List<Line> ways = new List<Line>();
            using (FileStream file = new FileStream(Folder + "\\LineWays.xml", FileMode.Open))
            {
                lWays = Deserialize(file);
                ways = Drawing(lWays);
            }
            return ways;
        }
    }
}
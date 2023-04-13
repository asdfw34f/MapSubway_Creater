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
    public class ImportStation
    {
        List<Station> elSt = new List<Station>();
        Canvas canvas= null;
        public ImportStation(Canvas canvas) 
        {
            this.canvas = canvas;
        }

        private List<Station> Deserialize(Stream stream)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Station>));
            List<Station> stations = formatter.Deserialize(stream) as List<Station>;

            return stations;
        }

        public List<Ellipse> Drawing(List<Station> sts)
        {
            List<Ellipse> els = new List<Ellipse>();
            BrushConverter brush = new BrushConverter();
            foreach (Station stat in sts)
            {
                DrawStation ds = new DrawStation()
                {
                    color = brush.ConvertFromString(stat.Color.ToString()) as Brush,
                    Pstart = stat.Position,
                    iditLoc = false
                };
                els.Add(ds.Draw());
            }
            return els;
        }

        public List<Ellipse> Import(string Folder)
        {
            List<Ellipse> stations = new List<Ellipse>();
            using (FileStream file = new FileStream(Folder + "\\Stations.xml", FileMode.Open))
            {
                elSt = Deserialize(file);
                if (elSt != null)
                {
                    stations = Drawing(elSt);
                    return stations;
                }
            }
            return null;
        }
    }
}
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
        Canvas canvas { get; set; } = null;

        public ImportStation(Canvas can) 
        {
            canvas = can;
        }

        private List<Station> Deserialize(Stream stream)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Station>));
            List<Station> stations = formatter.Deserialize(stream) as List<Station>;

            if (stations != null)
            {
                foreach (Station person in stations)
                {
                    stations.Add(person);
                }
                return stations;
            }
            return null;
        }

        private List<Ellipse> Drawing(List<Station> stations)
        {
            List<Ellipse> newSt = new List<Ellipse>();
            foreach (Station st in stations)
            {
                BrushConverter _conv= new BrushConverter();
                DrawStation ds = new DrawStation(canvas)
                {
                    Pstart = st.Position,
                    color = _conv.ConvertFromString(st.Color) as Brush,
                };
                newSt.Add(ds.Draw());
            }
            return newSt;
        }

        public List<Ellipse> Import(string Folder)
        {
            List<Station> stations = new List<Station>();

            using (FileStream file = new FileStream(Folder + "\\Stations.xml", FileMode.Open))
            {
                stations = Deserialize(file);
                if (stations != null)
                {
                    List<Ellipse> st = Drawing(stations);
                    return st;
                }
            }
            return null;
        }
    }
}
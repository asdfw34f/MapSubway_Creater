using DrawMapMetroLibrary.Atributs;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace AtributsSubwauLibrary.Import
{
    public class ImportStation
    {
        List<Station> elSt = new List<Station>();

        public ImportStation() 
        {
        }

        private List<Station> Deserialize(Stream stream)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Station>));
            List<Station> stations = formatter.Deserialize(stream) as List<Station>;

            if (stations != null)
            {
                foreach (Station st in stations)
                {
                    stations.Add(st);
                }
                return stations;
            }
            return null;
        }

        public List<Ellipse> Import(string Folder)
        {
            List<Ellipse> stations = new List<Ellipse>();
            using (FileStream file = new FileStream(Folder + "\\Stations.xml", FileMode.Open))
            {
                elSt = Deserialize(file);
                if (elSt != null)
                {
                    foreach (Station st in elSt)
                    {
                    }
                }
            }
            return null;
        }

        public List<Station> GetStations() 
        {
            return elSt;
        }
    }
}
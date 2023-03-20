using DrawMapMetroLibrary.Atributs;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace DrawMapMetroLibrary.Saving
{
    public class SaveStation
    {
        List<Station> stations = new List<Station>();

        public SaveStation() { }

        public void AddStation
            (string nameStation, int nextWay, int backWay, string NameWay, string brush, Point Position) 
        {
            stations.Add(new Station());
        }

        public void RemoveStation(string nameStation, string NameWay) 
        {
            int id = -1;
            foreach(Station i in stations)
            {
                id++;
                if (i.NameStation == nameStation && i.NameWay == NameWay)
                {
                    stations.RemoveAt(id);
                    return;
                }
            }
        }

        public void UpdateStatin(string nameStation, string NameWay, 
            string NEWnameStation, int NEWnextWay, int NEWbackWay,
            string NEWNameWay, string NEWbrush, Point Position)
        {
            int id = -1;
            foreach (Station i in stations)
            {
                id++;
                if (i.NameStation == nameStation && i.NameWay == NameWay)
                {
                    stations[id] = new Station()
                    { 
                        NameStation = NEWnameStation,
                        NameWay = NEWNameWay, 
                        Color = NEWbrush, 
                        BackWay = NEWbackWay,
                        NextWay = NEWnextWay, 
                        Position = Position 
                    };
                    return;
                }
            }
        }

        public void Save() 
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Station>));
            
            using (FileStream fs = new FileStream("Stations.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, stations);
            }
        }
    }
}
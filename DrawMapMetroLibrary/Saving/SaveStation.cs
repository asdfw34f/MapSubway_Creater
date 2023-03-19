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

        public void AddStation(string nameStation, int nextWay, 
            int backWay, string NameWay, Brush brush, Point Position) 
        {
            stations.Add(new Station(nameStation, nextWay, backWay, NameWay, brush, Position));
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
            string NEWNameWay, Brush NEWbrush, Point Position)
        {
            int id = -1;
            foreach (Station i in stations)
            {
                id++;
                if (i.NameStation == nameStation && i.NameWay == NameWay)
                {
                    stations[id] = new Station(NEWnameStation, NEWnextWay,
                        NEWbackWay, NEWNameWay, NEWbrush, Position);
                    return;
                }
            }
        }

        public void Save() 
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Station[]));

            using (FileStream fs = new FileStream("Stations.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, stations);
            }
        }
    }
}
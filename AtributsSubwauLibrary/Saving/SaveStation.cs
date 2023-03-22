using DrawMapMetroLibrary.Atributs;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace DrawMapMetroLibrary.Saving
{
    public class SaveStation
    {
        List<Station> stations = new List<Station>();

        public SaveStation() { }

        public void AddStation
            (string nameStation, double nextWay, double backWay, 
            string NameWay, string brush, Point Position) 
        {
            stations.Add(new Station()
            {
                NameStation= nameStation,
                NextWay= nextWay,
                BackWay= backWay,
                NameWay= NameWay,
                Color = brush,
                Position= Position
            });
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

        public void Save(string folder) 
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Station>));
            Station station = stations[0];
            using (FileStream fs = new FileStream(
                folder + "\\Stations.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, stations);
            }
        }
    }
}
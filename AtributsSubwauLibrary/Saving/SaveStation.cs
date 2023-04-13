using DrawMapMetroLibrary.Atributs;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace DrawMapMetroLibrary.Saving
{
    public class SaveStation
    {
        public List<Station> stations { get; set; } = new List<Station>();

        public SaveStation() { }
        /*
        public void AddStation
            (string nameStation, string nameWay,
            int back, int go, Point position, string color)
        {
            stations.Add(new Station()
            {
                NameWay = nameWay,
                NameStation = nameStation,
                Back= back,
                Go=go,
                Position = position,
                Color = color
            });
        }*/

        public void RemoveStation(string nameStation, string NameWay)
        {
            int id = -1;
            foreach (Station i in stations)
            {
                id++;
                if (i.NameStation == nameStation && i.NameWay == NameWay)
                {
                    stations.RemoveAt(id);
                    return;
                }
            }
        }

        public bool UpdateStatin(string nameStation, string NameWay,
            string NEWnameStation, Ellipse newSt, string NEWNameWay, int newBack, int newGo)
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
                        Back= newBack,
                        Go= newGo
                    };
                    return true;
                }
            }
            return false;
        }

        public void Save(string folder)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Station>));
            using (FileStream fs = new FileStream(
                folder + "\\Stations.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, stations);
            }
        }
    }
}
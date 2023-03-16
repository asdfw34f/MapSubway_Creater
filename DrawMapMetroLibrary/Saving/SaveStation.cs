using DrawMapMetroLibrary.Atributs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace DrawMapMetroLibrary.Saving
{
    public class SaveStation
    {
        Station[] stations = new Station[] { };

        int idx = 0;

        public SaveStation() { }

        public void AddStation(string nameStation, int nextWay, int backWay, string NameWay, Brush brush, Point Position) 
        {
            while (stations[idx] != null)
            {
                idx++;
            }
            stations[idx] = new Station(nameStation, nextWay, backWay, NameWay, brush, Position);
            idx++;
        }

        public void RemoveStation(string nameStation, string NameWay) 
        {
            int id = 0;
            foreach(Station i in stations)
            {
                if (i.NameStation == nameStation && i.NameWay == NameWay)
                {
                    stations[id] = null;
                    return;
                }
                id++;
            }
        }

        public void UpdateStatin(string nameStation, string NameWay, 
            string NEWnameStation, int NEWnextWay, int NEWbackWay,
            string NEWNameWay, Brush NEWbrush, Point Position)
        {
            int id = 0;
            foreach (Station i in stations)
            {
                if (i.NameStation == nameStation && i.NameWay == NameWay)
                {
                    stations[id] = new Station(NEWnameStation, NEWnextWay,
                        NEWbackWay, NEWNameWay, NEWbrush, Position);
                    return;
                }
                id++;
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
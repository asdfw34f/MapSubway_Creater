using DrawMapMetroLibrary.Atributs;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace DrawMapMetroLibrary.Saving
{
    public class SaveLineWay
    {
        List<LineWay> ways = new List<LineWay>();

        public SaveLineWay() { }

        public void AddWay
            (string NameWay, Point start, Point end, string color) 
        {
            ways.Add(new LineWay() 
            {
                NameWay = NameWay,
                Start = start,
                End = end,
                Color = color
            });
        }

        public void RemoveWay(string nameWay) 
        {
            int id = -1;
            foreach(LineWay i in ways)
            {
                id++;
                if (i.NameWay == nameWay)
                {
                    ways.RemoveAt(id);
                    return;
                }
            }
        }

        public void UpdateWay(string NameWay)
        {
            int id = -1;
            foreach (LineWay i in ways)
            {
                id++;
                if (i.NameWay == NameWay)
                {
                    ways[id] = new LineWay()
                    { 
                        NameWay = NameWay 
                    };
                    return;
                }
            }
        }

        public void Save(string folder) 
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<LineWay>));

            using (FileStream fs = new FileStream(
                folder + "\\LineWays.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, ways);
            }
        }
    }
}
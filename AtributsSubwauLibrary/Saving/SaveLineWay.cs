using DrawMapMetroLibrary.Atributs;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace DrawMapMetroLibrary.Saving
{
    public class SaveLineWay
    {
        List<LineWay> ways = new List<LineWay>();

        public SaveLineWay() { }

        public void AddWay(string NameWay, Point Start, Point End, string brush) 
        {
            ways.Add(new LineWay() 
            { 
                Color = brush,
                End = End, 
                Start = Start, 
                NameWay = NameWay
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

        public void UpdateWay(string NameWay, Point Start, Point End, string brush)
        {
            int id = -1;
            foreach (LineWay i in ways)
            {
                id++;
                if (i.NameWay == NameWay)
                {
                    ways[id] = new LineWay()
                    { 
                        Color = brush, 
                        End = End, 
                        Start = Start, 
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
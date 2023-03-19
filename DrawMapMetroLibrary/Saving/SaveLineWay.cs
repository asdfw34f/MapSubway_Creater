using DrawMapMetroLibrary.Atributs;
using System;
using System.Collections;
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
    public class SaveLineWay
    {
        List<LineWay> ways = new List<LineWay>();

        public SaveLineWay() { }

        public void AddWay(string NameWay, Point Start, Point End, Brush brush) 
        {
            ways.Add(new LineWay(NameWay, Start, End, brush));
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

        public void UpdateWay(string NameWay, Point Start, Point End, Brush brush)
        {
            int id = -1;
            foreach (LineWay i in ways)
            {
                id++;
                if (i.NameWay == NameWay)
                {
                    ways[id] = new LineWay(NameWay, Start, End, brush);
                    return;
                }
            }
        }

        public void Save() 
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Station[]));

            using (FileStream fs = new FileStream("LineWays.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, ways);
            }
        }
    }
}
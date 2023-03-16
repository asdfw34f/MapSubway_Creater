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
        LineWay[] ways = new LineWay[] { };

        int idx = 0;

        public SaveLineWay() { }

        public void AddWay(string NameWay, Point Start, Point End, Brush brush) 
        {
            while (ways[idx] != null)
            {
                idx++;
            }
            ways[idx] = new LineWay(NameWay, Start, End, brush);
            idx++;
        }

        public void RemoveWay(string nameWay) 
        {
            int id = 0;
            foreach(LineWay i in ways)
            {
                if (i.NameWay == nameWay)
                {
                    ways[id] = null;
                    return;
                }
                id++;
            }
        }

        public void UpdateWay(string NameWay, Point Start, Point End, Brush brush)
        {
            int id = 0;
            foreach (LineWay i in ways)
            {
                if (i.NameWay == NameWay)
                {
                    ways[id] = null;
                    return;
                }
                id++;
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
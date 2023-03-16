using DrawMapMetroLibrary.Atributs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Xml.Serialization;

namespace DrawMapMetroLibrary.Saving
{
    public class SaveEllipseWay
    {
        EllipseWay[] ways = new EllipseWay[] { };

        int idx = 0;

        public SaveEllipseWay() { }

        public void AddWay(string NameWay, Point Position, Brush brush, double Height, double Width)
        {
            while (ways[idx] != null)
            {
                idx++;
            }
            ways[idx] = new EllipseWay(NameWay, Position, brush, Height, Width);
            idx++;
        }

        public void RemoveWay(string nameWay)
        {
            int id = 0;
            foreach (EllipseWay i in ways)
            {
                if (i.NameWay == nameWay)
                {
                    ways[id] = null;
                    return;
                }
                id++;
            }
        }

        public void UpdateWay(string NameWay, Point Position, Brush brush, double Height, double Width)
        {
            int id = 0;
            foreach (EllipseWay i in ways)
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

            using (FileStream fs = new FileStream("EllipseWays.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, ways);
            }
        }
    }
}
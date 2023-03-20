using DrawMapMetroLibrary.Atributs;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows;
using System.Xml.Serialization;

namespace DrawMapMetroLibrary.Saving
{
    public class SaveEllipseWay
    {
        List<EllipseWay> ways = new List<EllipseWay>();
        public SaveEllipseWay() { }

        public void AddWay(string NameWay, Point Position, string brush, double Height, double Width)
        {
            ways.Add(new EllipseWay()
            {
                Color = brush,
                Height = Height,
                Width = Width,
                Position = Position,
                NameWay = NameWay
            });
        }

        public void RemoveWay(string nameWay)
        {
            int id = -1;
            foreach (EllipseWay i in ways)
            {
                id++;
                if (i.NameWay == nameWay)
                {
                    ways.RemoveAt(id);
                    return;
                }
            }
        }

        public void UpdateWay(string NameWay, Point Position,
            string brush, double Height, double Width)
        {
            int id = -1;
            foreach (EllipseWay i in ways)
            {
                id++;
                if (i.NameWay == NameWay)
                {
                    ways[id].NameWay = NameWay;
                    ways[id].Position = Position;
                    ways[id].Color = brush;
                    ways[id].Width = Width;
                    ways[id].Height = Height;

                    return;
                }
            }
        }

        public void Save()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<EllipseWay>));

            using (FileStream fs = new FileStream(
                "C:\\Users\\Daniil\\Desktop\\EllipseWays.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, ways);
            }
        }
    }
}
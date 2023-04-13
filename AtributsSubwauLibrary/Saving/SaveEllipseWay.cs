using DrawMapMetroLibrary.Atributs;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace DrawMapMetroLibrary.Saving
{
    public class SaveEllipseWay
    {
        public List<EllipseWay> ways { get; set; } = new List<EllipseWay>();
        public SaveEllipseWay() { }
        /*
        public void AddWay(
            string NameWay, Point position, string color, 
            double height, double width)
        {
            ways.Add(new EllipseWay()
            {
                NameWay = NameWay,
                Position = position,
                Color = color,
                Height = height,
                Width = width
            });
        }*/

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

        public void UpdateWay(string NameWay, Ellipse ellipse)
        {
            int id = -1;
            foreach (EllipseWay i in ways)
            {
                id++;
                if (i.NameWay == NameWay)
                {
                    ways[id].NameWay = NameWay;

                    return;
                }
            }
        }

        public void Save(string folder)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<EllipseWay>));

            using (FileStream fs = new FileStream(
                folder + "\\EllipseWays.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, ways);
            }
        }
    }
}
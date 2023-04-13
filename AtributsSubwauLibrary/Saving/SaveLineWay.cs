using DrawMapMetroLibrary.Atributs;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace DrawMapMetroLibrary.Saving
{
    public class SaveLineWay
    {
        public List<LineWay> ways { get; set; } = new List<LineWay>();

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
            foreach (LineWay i in ways)
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

        public bool Save(string folder)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<LineWay>));
            try
            {
                using (FileStream fs = new FileStream
                    (folder + "\\LineWays.xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, ways);

                }
                return true;
            }
            catch (IOException e)
            {
                MessageBox.Show(
                    e.Message,
                    "Ошибка сохранения линии метро");
                return false;
            }

        }
    }
}
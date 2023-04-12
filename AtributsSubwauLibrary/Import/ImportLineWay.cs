using DrawMapMetroLibrary.Atributs;
using System.Collections.Generic;
using System.IO;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace AtributsSubwauLibrary.Import
{
    public class ImportLineWay
    {
        List<LineWay> lWays = new List<LineWay>();

        public ImportLineWay()
        {
        }

        private List<LineWay> Deserialize(Stream stream)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<LineWay>));
            List<LineWay> ways = formatter.Deserialize(stream) as List<LineWay>;

            if (ways != null)
            {
                foreach (LineWay way in ways)
                {
                    ways.Add(way);
                }
                return ways;
            }
            return null;
        }

        public List<Line> Import(string Folder)
        {
            List<Line> ways = new List<Line>();
            using (FileStream file = new FileStream(Folder + "\\LineWays.xml", FileMode.Open))
            {
                lWays = Deserialize(file);
                if (lWays != null)
                {
                    foreach(LineWay way in lWays)
                    {
                    }
                    return ways;
                }
            }
            return null;
        }

        public List<LineWay> GetLineWays()
        {
            return lWays;
        }
    }
}
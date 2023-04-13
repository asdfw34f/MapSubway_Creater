using AtributsSubwauLibrary.Atributs;
using DrawMapMetroLibrary.Atributs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace AtributsSubwauLibrary.Saving
{
    public class Split
    {
        AllMap map;

        public Split(List<Station> stations, List<EllipseWay> eways, List<LineWay> lways ) 
        {
            map= new AllMap()
            {
                stations = stations,
                lways = lways,
                eways = eways
            };
        }

        public void Save(string folder)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(AllMap));
            using (FileStream fs = new FileStream(
                folder + "\\MapMetro.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, map);
            }
        }
    }
}
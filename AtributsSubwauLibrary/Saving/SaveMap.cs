using AtributsSubwauLibrary.Atributs;
using DrawMapMetroLibrary.Atributs;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace AtributsSubwauLibrary.Saving
{
    public class SaveMap
    {
        AllMap map;

        public SaveMap(List<Station> stations, List<EllipseWay> eways, List<LineWay> lways ) 
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
                folder + "\\MapMetro.xml", FileMode.Create))
            {
                formatter.Serialize(fs, map);
            }
        }
    }
}
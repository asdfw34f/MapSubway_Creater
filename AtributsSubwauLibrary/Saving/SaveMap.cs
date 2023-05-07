using AtributsSubwayLibrary.Model;
using System.IO;
using System.Xml.Serialization;

namespace AtributsSubwayLibrary.Saving
{
    public class SaveMap
    {
        RouteSubway subway;

        public SaveMap(RouteSubway subway) 
        {
            this.subway = subway;
        }

        public void Save(string folder)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(RouteSubway));
            using (FileStream fs = new FileStream(
                folder + "\\MapMetro.xml", FileMode.Create))
            {
                formatter.Serialize(fs, subway);
            }
        }
    }
}
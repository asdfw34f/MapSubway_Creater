using EditorSubwayMap.MVVM.Model;
using System.IO;
using System.Xml.Serialization;

namespace EditorSubwayMap.Infrastructure.IORoute
{
    public class SaveMap
    {
        Route subway;

        public SaveMap(Route subway)
        {
            this.subway = subway;
        }

        public void Save(string folder)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Route));
            using (FileStream fs = new FileStream(
                folder + "\\MapMetro.xml", FileMode.Create))
            {
                formatter.Serialize(fs, subway);
            }
        }
    }
}
using DrawMapMetroLibrary.Atributs;
using System;
using System.Collections.Generic;

namespace AtributsSubwauLibrary.Atributs
{
    [Serializable()]
    public class AllMap
    {
        public List<Station> stations { get; set; } = new List<Station>();
        public List<EllipseWay> eways { get; set; } = new List<EllipseWay>();
        public List<LineWay> lways { get; set; } = new List<LineWay>();
        public AllMap() { }
    }
}

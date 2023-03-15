using EditorSubwayMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorSubwayMap.Atributs
{
    internal class Station
    {
        string NameStation { get; set; }
        int NextWay { get; set; }
        int BackWay { get; set; }
        int idLine { get; set; }

        internal Station(string nameStation, int nextWay, int backWay, int idLine)
        {
            NameStation = nameStation;
            NextWay = nextWay;
            BackWay = backWay;
            this.idLine = idLine;
        }
    }
}

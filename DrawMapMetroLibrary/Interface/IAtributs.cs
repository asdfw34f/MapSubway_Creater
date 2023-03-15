using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EditorSubwayMap.Interface
{
    internal interface IAtributs
    {
        string Name { get; set; }
        int Way { get; set; }
        Brush Color { get; set; }

        string GetName();
        int GetWay();
        Brush GetColor();
    }
}

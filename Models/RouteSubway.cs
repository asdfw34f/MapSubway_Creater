using AtributsSubwauLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorSubwayMap.Models
{
    public class RouteSubway
    {
        List<CircleWay> CircleWays { get; set; }
        List<LineWay> LineWays { get; set; }
    }
}
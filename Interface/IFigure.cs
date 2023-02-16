using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace EditorSubwayMap
{
    internal interface IFigure
    {
        Point Pstart  { set; }
        Point Pend { set; }
        SolidColorBrush color { set; }
    }
}

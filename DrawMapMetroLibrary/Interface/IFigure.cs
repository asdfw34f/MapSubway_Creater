// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

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
        Point Pstart  { get; set; }
        Point Pend { get; set; }
        Brush color { get; set; }
    }
}
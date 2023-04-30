using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorSubwayMap.Data
{
    internal static class DrawingMode
    {
        private static string _DrawMode;
        public static string DrawMode
        {
            get => _DrawMode;
            set
            {
                if (value == "Line" || value == "Circle" || value == "Station")
                {
                    _DrawMode = value;
                }
            }
        }
    }
}
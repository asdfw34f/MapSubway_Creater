using EditorSubwayMap.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EditorSubwayMap.Scripts
{
    internal class ALine : IAtributs
    {
        private string lName;
        private int lWay;
        private SolidColorBrush lColor;
        private Line line;

        ALine(Line Line)
        {
            line = Line;
        }

        public string Name 
        { 
            get { return lName; }
            set => lName = value; 
        }

        public int Way
        {
            get { return lWay; }
            set => lWay = value;
        }

        public SolidColorBrush Color
        {
            get { return lColor; }
            set => lColor = value;
        }

        public Brush GetColor()
        {
            return line.Fill;
        }

        public string GetName()
        {
            return line.Name;
        }

        public int GetWay()
        {
            return lWay;
        }
    }
}

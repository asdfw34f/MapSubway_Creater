using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EditorSubwayMap.Model
{
    public class DrawLine : IFigure
    {

        public DrawLine() { }
        Point Pstart1 = new Point(0, 0);
        Point Pend1 = new Point(0, 0);

        public Point Pstart
        {
            get
            {
                return Pstart1;
            }
            set
            {
                Pstart1 = value;
            }
        }

        public Point Pend
        {
            get
            {
                return Pend1;
            }
            set
            {
                Pend1 = value;
            }
        }

        public SolidColorBrush color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        public Line Draw()
        {
            Line newLine = new Line()
            {
                Stroke = color,
                StrokeThickness = 7
            };
            newLine.X1 = Pstart1.X;
            newLine.Y1 = Pstart1.Y;
            newLine.X2 = Pend1.X;
            newLine.Y2 = Pend1.Y;

            return newLine;
        }
    }
}

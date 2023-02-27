// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
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
        private Point Pstart1;
        private Point Pend1;
        private SolidColorBrush color1;

        public DrawLine() 
        {
            Pstart1 = new Point(0, 0);
            Pend1 = new Point(0, 0);
            color1 = Brushes.Black;
        }

        public Point Pstart
        {
            get => Pstart1;
            set
            {
                Pstart1 = value;
            }
        }

        public Point Pend
        {
            get => Pend1;
            set
            {
                Pend1 = value;
            }
        }

        public SolidColorBrush color
        {
            get => color1;
            set
            {
                color1 = value;
            }
        }

        public Line Draw()
        {
            Line newLine = new Line()
            {
                Stroke = color1,
                StrokeThickness = 7,
                Cursor = Cursors.Hand
            };
            newLine.X1 = Pstart1.X;
            newLine.Y1 = Pstart1.Y;
            newLine.X2 = Pend1.X;
            newLine.Y2 = Pend1.Y;

            return newLine;
        }
    }
}

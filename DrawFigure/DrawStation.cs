using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace EditorSubwayMap.DrawFigure
{
    internal class DrawStation : IFigure
    {
        private Point Pstart1;
        private Point Pend1;
        private SolidColorBrush color1;

        public DrawStation()
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

        public Ellipse Draw()
        {
            Ellipse newSt = new Ellipse()
            {
                Fill = Brushes.White,
                Stroke = color1,
                Height = 20,
                Width = 20,
                StrokeThickness = 5,
                Margin = new Thickness(Pstart1.X, Pstart1.Y, Pstart1.X + 20, Pstart1.Y + 20)

            };

            return newSt;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;

namespace EditorSubwayMap.DrawFigure
{
    internal class DrawStation : IFigure
    {
        private Point pStart;
        private Point currentPoint;
        private SolidColorBrush color1;

        public DrawStation()
        {
            pStart = new Point(0, 0);
            currentPoint = new Point(0, 0);
            color1 = Brushes.Black;
        }

        public Point Pstart
        {
            get => pStart;
            set
            {
                pStart = value;
            }
        }

        public Point Pend
        {
            get => currentPoint;
            set
            {
                currentPoint = value;
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
                Cursor = Cursors.Hand,
                StrokeThickness = 5,
                Margin = new Thickness(
                    pStart.X, pStart.Y,
                    pStart.X + 20, pStart.Y + 20)
            };

            return newSt;
        }
    }
}

// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
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
    internal class DrawEllipse : IFigure
    {
        private Point pStart;
        private Point currentPoint;
        private SolidColorBrush color1;

        public DrawEllipse()
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
                Stroke = color1,
                Height = 20,
                Width = 20,
                StrokeThickness = 5,
                Margin = new Thickness(
                    pStart.X, pStart.Y,
                    pStart.X + 20, pStart.Y + 20)
            };
            
            return newSt;
        }

        public Ellipse Edit(Ellipse ellipse)
        {
            Point ellipsePoint = new Point();

            if (currentPoint.X >= pStart.X)
            {
                ellipsePoint.X = pStart.X;
                ellipse.Width = currentPoint.X - pStart.X;
            }
            else
            {
                ellipsePoint.X = currentPoint.X;
                ellipse.Width = pStart.X - currentPoint.X;
            }
            if (currentPoint.Y >= pStart.Y)
            {
                ellipsePoint.Y = pStart.Y;
                ellipse.Height = currentPoint.Y - pStart.Y;
            }
            else
            {
                ellipsePoint.Y = currentPoint.Y;
                ellipse.Height = pStart.Y - currentPoint.Y;
            }

            ellipse.Margin = new Thickness(
                ellipsePoint.X, 
                ellipsePoint.Y, 
                ellipse.Margin.Left, 
                ellipse.Margin.Top);

            return ellipse;
        }
    }
}

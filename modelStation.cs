using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;

namespace EditorSubwayMap.model
{
    internal class modelStation
    {
        private double _disEll = double.MaxValue;
        private double _disLine = double.MaxValue;
        private Point _nearestPointEll;
        private Point _nearestPointLine;

        internal modelStation(Ellipse station, List<Line> lineways, List<Ellipse> ellipseways)
        {
            double disL = MoveEllipseToNearestLine(station, lineways);
            double disE = MoveEllipseToNearestEllipse(station, ellipseways);
            if (disL == double.MaxValue && disL == double.MaxValue)
                return;
            else 
            {
                if (_disLine < _disEll)
                {
                    // Перемещаем Ellipse в ближайшую точку на ближайшей Line
                    Canvas.SetLeft(station, _nearestPointLine.X - station.ActualWidth / 2);
                    Canvas.SetTop(station, _nearestPointLine.Y - station.ActualHeight / 2);
                }
                else
                {
                    // Перемещаем Ellipse в ближайшую точку на ближайшем контуре Ellipse
                    Canvas.SetLeft(station, _nearestPointEll.X - station.ActualWidth / 2);
                    Canvas.SetTop(station, _nearestPointEll.Y - station.ActualHeight / 2);
                }
            }
        }

        private double MoveEllipseToNearestLine(Ellipse ellipse, List<Line> lines)
        {
            if (lines == null)
            {
                return double.MaxValue;
            }
            // Проходим по всем Line на Canvas
            foreach (Line line in lines)
            {
                // Находим ближайшую точку на Line к Ellipse
                double lineX1 = line.X1;
                double lineY1 = line.Y1;
                double lineX2 = line.X2;
                double lineY2 = line.Y2;
                double ellipseX = Canvas.GetLeft(ellipse) + ellipse.ActualWidth / 2;
                double ellipseY = Canvas.GetTop(ellipse) + ellipse.ActualHeight / 2;

                double a2b2 = (lineX2 - lineX1) * (lineX2 - lineX1) + (lineY2 - lineY1) * (lineY2 - lineY1);
                double r2 = ((ellipseX - lineX1) * (lineX2 - lineX1) + (ellipseY - lineY1) * (lineY2 - lineY1)) / a2b2;

                double pointX, pointY;
                if (r2 < 0)
                {
                    pointX = lineX1;
                    pointY = lineY1;
                }
                else if (r2 > 1)
                {
                    pointX = lineX2;
                    pointY = lineY2;
                }
                else
                {
                    pointX = lineX1 + r2 * (lineX2 - lineX1);
                    pointY = lineY1 + r2 * (lineY2 - lineY1);
                }

                // Вычисляем расстояние между ближайшей точкой и Ellipse
                double distance = Math.Sqrt((ellipseX - pointX) *
                    (ellipseX - pointX) + (ellipseY - pointY) * (ellipseY - pointY));

                // Если расстояние меньше текущего минимального расстояния,
                // то запоминаем новое минимальное расстояние и ближайшую точку
                if (distance < _disLine)
                {
                    _disLine = distance;
                    _nearestPointLine = new Point(pointX, pointY);
                }
            }
            return _disLine;
        }

        private double MoveEllipseToNearestEllipse(Ellipse ellipse, List<Ellipse> ellipses)
        {
            if (ellipses == null)
            {
                return double.MaxValue;
            }

            // Проходим по всем Ellipse на Canvas, за исключением текущей
            foreach (Ellipse otherEllipse in ellipses)
            {
                // Получаем границы текущей и другой Ellipse
                Rect ellipseBounds = ellipse.RenderTransform.TransformBounds(new Rect(ellipse.RenderSize));
                Rect otherEllipseBounds = otherEllipse.RenderTransform.TransformBounds(new Rect(otherEllipse.RenderSize));

                // Находим центр Ellipse и другой Ellipse
                Point ellipseCenter = new Point(ellipseBounds.Left + ellipseBounds.Width / 2,
                    ellipseBounds.Top + ellipseBounds.Height / 2);

                Point otherEllipseCenter = new Point(otherEllipseBounds.Left + otherEllipseBounds.Width / 2,
                    otherEllipseBounds.Top + otherEllipseBounds.Height / 2);

                // Вычисляем расстояние между центром Ellipse и другой Ellipse
                double distance = Math.Sqrt((ellipseCenter.X - otherEllipseCenter.X) *
                    (ellipseCenter.X - otherEllipseCenter.X) + (ellipseCenter.Y - otherEllipseCenter.Y) *
                    (ellipseCenter.Y - otherEllipseCenter.Y));

                // Если расстояние меньше текущего минимального расстояния,
                // то запоминаем новое минимальное расстояние и ближайшую точку
                if (distance < _disEll)
                {
                    _disEll = distance;
                    _nearestPointEll = otherEllipseCenter;
                }
            }
            return _disEll;
        }
    }
}
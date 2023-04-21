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
        internal void MoveEllipseToNearestLine(Ellipse ellipse, List<Line> lines)
        {
            double minDistance = double.MaxValue;
            Point nearestPoint = new Point();

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
                double distance = Math.Sqrt((ellipseX - pointX) * (ellipseX - pointX) + (ellipseY - pointY) * (ellipseY - pointY));

                // Если расстояние меньше текущего минимального расстояния,
                // то запоминаем новое минимальное расстояние и ближайшую точку
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestPoint = new Point(pointX, pointY);
                }
            }

            // Перемещаем Ellipse в ближайшую точку на ближайшей Line
            Canvas.SetLeft(ellipse, nearestPoint.X - ellipse.ActualWidth / 2);
            Canvas.SetTop(ellipse, nearestPoint.Y - ellipse.ActualHeight / 2);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using DrawMapMetroLibrary.Atributs;

namespace EditorSubwayMap.model
{
    internal class modelStation
    {
        private double _disEll = double.MaxValue;
        private double _disLine = double.MaxValue;

        private Point _nearestPointonEll = new Point();
        private Point _nearestPointLine;
        private Ellipse _nearestEllipse = new Ellipse();

        private Ellipse station { get; set; }
        private List<Line> lineways { get; set; }
        private List<Ellipse> ellipseways { get; set; }

        internal modelStation(Ellipse station, List<Line> lineways, List<Ellipse> ellipseways)
        {
            this.station = station;
            this.lineways = lineways;
            this.ellipseways = ellipseways;
        }

        internal Point GetPoint()
        {
            double disL = MoveEllipseToNearestLine(station, lineways);
            double disE = MoveEllipseToNearestEllipse(station, ellipseways);

            if (disL == double.MaxValue && disE == double.MaxValue)
                return new Point(0, 0);
            if (_disLine < _disEll)
            {
                // Перемещаем Ellipse в ближайшую точку на ближайшей Line
                return new Point(
                            _nearestPointLine.X - station.ActualWidth / 2,
                            _nearestPointLine.Y - station.ActualHeight / 2
                            );
            }
            else
            {
                // Перемещаем Ellipse в ближайшую точку на ближайшем контуре Ellipse
                //Canvas.SetLeft(station, Canvas.GetLeft(_nearestEllipse) + _nearestPointonEll.X - station.Width / 2);
                //Canvas.SetTop(station, Canvas.GetTop(_nearestEllipse) + _nearestPointonEll.Y - station.Height / 2);
                Point n =  new Point(
                            Canvas.GetLeft(_nearestEllipse) + _nearestPointonEll.X - station.Width / 2,
                            Canvas.GetTop(_nearestEllipse) + _nearestPointonEll.Y - station.Height / 2
                    );
                return n;
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
            // список круговых веток метро
            List<Ellipse> circles = ellipses;

            // координаты новой станции
            double stationX = Canvas.GetLeft(ellipse);
            double stationY = Canvas.GetTop(ellipse);

            // поиск ближайшей круговой ветки метро
            double nearestDistance = double.MaxValue;

            foreach (Ellipse circle in circles)
            {
                // координаты центра круга
                double centerX = Canvas.GetLeft(circle) + circle.Width / 2;
                double centerY = Canvas.GetTop(circle) + circle.Height / 2;

                // расстояние от станции до центра круга
                double distance = Math.Sqrt(Math.Pow((centerX - stationX), 2) + Math.Pow((centerY - stationY), 2));

                if (distance < nearestDistance)
                {
                    _nearestEllipse = circle;
                    nearestDistance = distance;
                }
            }

            // координаты станции на ближайшей круговой ветке метро
            _nearestPointonEll = new Point(
                (_nearestEllipse.Width / 2) * (stationX - Canvas.GetLeft(_nearestEllipse)) / (_nearestEllipse.Width / 2),
                (_nearestEllipse.Height / 2) * (stationY - Canvas.GetTop(_nearestEllipse)) / (_nearestEllipse.Height / 2)
                );

            return nearestDistance;
        }
    }
}
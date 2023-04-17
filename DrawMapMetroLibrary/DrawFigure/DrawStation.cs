// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace EditorSubwayMap.DrawFigure
{
    public class DrawStation
    {
        private Point pStart;
        private Brush color1;
        private bool _isDragging;
        private Point _lastPosition;

        public DrawStation()
        {
            pStart = new Point(0, 0);
        }

        public Canvas canvas { get; set; }

        /// <summary>
        /// Сводка:
        ///      Устанавливает позицию станции по канвасу.
        /// </summary>
        /// <returns>
        ///      Возвращает текущую позицию станции 
        ///      по канвасу (изначально равен 0; 0).
        /// </returns>
        public Point Pstart
        {
            get => pStart;
            set => pStart = value;
        }

        /// <summary>
        /// Сводка:
        ///      Устанавливает цвет станции.
        /// </summary>
        /// <returns>
        ///      Возвращает текущий цвет станции.
        /// </returns>
        public Brush color
        {
            get => color1;
            set => color1 = value;
        }

        /// <summary>
        /// Сводка:
        ///      Рисует эллипс станции по заданым параметрам.
        /// </summary>
        /// <returns>
        ///      Возвращает готовую станцию.
        /// </returns>
        public Ellipse Draw()
        {
            Ellipse newSt = new Ellipse()
            {
                Fill = null,
                Stroke = color1,
                Height = 20,
                Width = 20,
                Cursor = Cursors.Hand,
                StrokeThickness = 5,
                Margin = new Thickness(0)
            };

            Canvas.SetLeft(newSt, Pstart.X);
            Canvas.SetTop(newSt, Pstart.Y);
            newSt.Loaded += (sender, args) =>
            {
                Ellipse st = sender as Ellipse;
                Point p = new Point(Canvas.GetLeft(st), Canvas.GetTop(st));
            };

            newSt.MouseLeftButtonDown += station_MouseLeftButtonDown;
            newSt.MouseMove += station_MouseMove;
            newSt.MouseLeftButtonUp += station_MouseLeftButtonUp;
            newSt.AllowDrop = true;

            return newSt;
        }

        private void station_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isDragging = true;
            _lastPosition = e.GetPosition(canvas);
            Mouse.Capture((Ellipse)sender);
        }

        private void station_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Вычисляем новые координаты эллипса
                Point currentPosition = e.GetPosition(canvas);
                double dx = currentPosition.X - _lastPosition.X;
                double dy = currentPosition.Y - _lastPosition.Y;
                double newLeft = Canvas.GetLeft((Ellipse)sender) + dx;
                double newTop = Canvas.GetTop((Ellipse)sender) + dy;

                // Поддерживаем эллипс внутри Canvas
                if (newLeft < 0)
                    newLeft = 0;
                if (newTop < 0)
                    newTop = 0;
                if (newLeft > canvas.ActualWidth)
                    newLeft = canvas.ActualWidth;
                if (newTop > canvas.ActualHeight)
                    newTop = canvas.ActualHeight;

                // Устанавливаем новые координаты эллипса
                Canvas.SetLeft((Ellipse)sender, newLeft);
                Canvas.SetTop((Ellipse)sender, newTop);
                _lastPosition = currentPosition;
            }
        }

        private void station_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            Mouse.Capture(null);
        }
    }
}
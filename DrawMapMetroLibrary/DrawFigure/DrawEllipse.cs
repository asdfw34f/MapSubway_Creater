// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;

namespace EditorSubwayMap.DrawFigure
{
    public class DrawEllipse
    {
        private Point pStart;
        private Point currentPoint;
        private Brush color1;
        private double Height1 = 20;
        private double Width1 = 20;

        private bool _isDragging;
        private Point _lastPosition;

        public DrawEllipse()
        {
            pStart = new Point(0, 0);
            currentPoint = new Point(0, 0);
        }

        public Canvas canvas { get;set; }

        public double Height { get => Height1; set => Height1 = value; }
        public double Width { get => Width1; set => Width1 = value; }

        /// <summary>
        /// Сводка:
        ///      Устанавливает начальные координаты позиции круговой
        ///      ветки метро по канвасу.
        /// </summary>
        /// <returns>
        ///      Возвращает текущие начальные позицию круговой 
        ///      ветки метро по канвасу (изначально равен 0; 0).
        /// </returns>
        public Point Pstart
        {
            get => pStart;
            set => pStart = value;
        }

        /// <summary>
        /// Сводка:
        ///      Устанавливает конечные координаты 
        ///      позиции круговой ветки метро по канвасу.
        /// </summary>
        /// <returns>
        ///      Возвращает текущие конечную позицию круговой 
        ///      ветки метро по канвасу (изначально равен 0; 0).
        /// </returns>
        public Point currentP
        {
            get => currentPoint;
            set => currentPoint = value;
        }

        /// <summary>
        /// Сводка:
        ///      Устанавливает цвет круговой ветки метро.
        /// </summary>
        /// <returns>
        ///      Возвращает текущий цвет круговой ветки метро.
        /// </returns>
        public Brush color
        {
            get => color1;
            set => color1 = value;
        }

        /// <summary>
        /// Сводка:
        ///      Рисует эллипс круговой ветки метро по заданым параметрам.
        /// </summary>
        /// <returns>
        ///      Возвращает готовую ветку метро.
        /// </returns>
        public Ellipse Draw()
        {
            Ellipse newEl = new Ellipse()
            {
                Stroke = color1,
                Width = Width1,
                Height = Height1,
                Margin = new Thickness(0),
                StrokeThickness = 5,
                Cursor= Cursors.Hand,
                Fill = null
            };

            Canvas.SetLeft(newEl, Pstart.X);
            Canvas.SetTop(newEl, Pstart.Y);

            newEl.MouseLeftButtonDown += ellipse_MouseLeftButtonDown;
            newEl.MouseMove += ellipse_MouseMove;
            newEl.MouseLeftButtonUp += ellipse_MouseLeftButtonUp;
            newEl.AllowDrop = true;
            
            return newEl;
        }

        /// <summary>
        /// Сводка:
        ///      Изменяет размер ветки по новым параметрам.
        /// </summary>
        /// <returns>
        ///      Возвращает изменённую ветки.
        /// </returns>
        public Ellipse EditSize(Ellipse ellipse)
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
            
            Canvas.SetLeft(ellipse, Pstart.X);
            Canvas.SetTop(ellipse, Pstart.Y);

            ellipse.Height = ellipse.Width;
            
            return ellipse;
        }

        private void ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isDragging = true;
            _lastPosition = e.GetPosition(canvas);
            Mouse.Capture((Ellipse)sender);
        }

        private void ellipse_MouseMove(object sender, MouseEventArgs e)
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
                    newLeft =canvas.ActualWidth;
                if (newTop > canvas.ActualHeight)
                    newTop = canvas.ActualHeight;

                // Устанавливаем новые координаты эллипса
                Canvas.SetLeft((Ellipse)sender, newLeft);
                Canvas.SetTop((Ellipse)sender, newTop);
                _lastPosition = currentPosition;
            }
        }

        private void ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            _isDragging = false;
            Mouse.Capture(null);
        }
    }
}

/*
 private bool isDragging;
private Point lastPosition;

private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
{
    isDragging = true;
    lastPosition = e.GetPosition(Canvas);
    Mouse.Capture((Ellipse)sender);
}

private void Ellipse_MouseMove(object sender, MouseEventArgs e)
{
    if (isDragging)
    {
        // Вычисляем новые координаты эллипса
        Point currentPosition = e.GetPosition(Canvas);
        double dx = currentPosition.X - lastPosition.X;
        double dy = currentPosition.Y - lastPosition.Y;
        double newLeft = Canvas.GetLeft((Ellipse)sender) + dx;
        double newTop = Canvas.GetTop((Ellipse)sender) + dy;

        // Поддерживаем эллипс внутри Canvas
        if (newLeft < 0) newLeft = 0;
        if (newTop < 0) newTop = 0;
        if (newLeft > Canvas.ActualWidth) newLeft = Canvas.ActualWidth;
        if (newTop > Canvas.ActualHeight) newTop = Canvas.ActualHeight;

        // Устанавливаем новые координаты эллипса
        Canvas.SetLeft((Ellipse)sender, newLeft);
        Canvas.SetTop((Ellipse)sender, newTop);
        lastPosition = currentPosition;
    }
}

private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
{
    isDragging = false;
    Mouse.Capture(null);
}
 */
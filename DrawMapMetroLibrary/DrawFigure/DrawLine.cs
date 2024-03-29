﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EditorSubwayMap.Model
{
    public class DrawLine
    {
        private Point Pstart1;
        private Point Pend1;
        private Brush color1;
        private bool _isDragging;
        private Point _lastPosition;
        public Line upLine;
        public DrawLine() 
        {
            Pstart1 = new Point(0, 0);
            Pend1 = new Point(0, 0);
        }

        public Canvas canvas { get; set; }

        /// <summary>
        /// Сводка:
        ///      Устанавливает позицию начала линии маршрута.
        /// </summary>
        /// <returns>
        ///      Возвращает текущую позицию начала линии 
        ///      маршрута (изначально равен 0; 0).
        /// </returns>
        public Point Pstart
        {
            get => Pstart1;
            set => Pstart1 = value;
        }

        /// <summary>
        /// Сводка:
        ///      Устанавливает позицию конца линии маршрута.
        /// </summary>
        /// <returns>
        ///      Возвращает текущую позицию конца 
        ///      линии маршрута (изначально равен 0; 0).
        /// </returns>
        public Point Pend
        {
            get => Pend1;
            set => Pend1 = value;
        }

        /// <summary>
        /// Сводка:
        ///      Устанавливает цвет линии маршрута.
        /// </summary>
        /// <returns>
        ///      Возвращает текущий цвет линии маршрута.
        /// </returns>
        public Brush color
        {
            get => color1;
            set => color1 = value;
        }

        /// <summary>
        /// Сводка:
        ///      Рисует линию по заданым параметрам.
        /// </summary>
        /// <returns>
        ///      Возвращает готовую линию маршрута.
        /// </returns>
        public Line Draw()
        {
            Line newLine = new Line()
            {
                Stroke = color1,
                Fill= color1,
                StrokeThickness = 7,
                Cursor = Cursors.Hand,
                Margin = new Thickness(0),
                X1 = Pstart1.X,
                Y1 = Pstart1.Y,
                X2 = Pend1.X,
                Y2 = Pend1.Y
            };

            newLine.MouseLeftButtonDown += NewLine_MouseLeftDown;
            newLine.MouseLeftButtonUp += NewLine_MouseLeftUp;
            newLine.MouseMove += NewLine_MouseMove;
         
            return newLine;
        }

        private void NewLine_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // перемещаем линию при перемещении мыши с захваченной линией
                Point endPoint = e.GetPosition(canvas);

                double offsetX = endPoint.X - _lastPosition.X;
                double offsetY = endPoint.Y - _lastPosition.Y;

                (sender as Line).X1 += offsetX;
                (sender as Line).Y1 += offsetY;
                (sender as Line).X2 += offsetX;
                (sender as Line).Y2 += offsetY;

                _lastPosition = endPoint;
            }
        }

        private void NewLine_MouseLeftUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            (sender as Line).ReleaseMouseCapture();

            upLine = sender as Line;
        }

        private void NewLine_MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Line line)
            {
                // захватываем линию при нажатии на нее левой кнопкой мыши
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    _isDragging = true;
                    _lastPosition = e.GetPosition(canvas);
                    line.CaptureMouse();
                }
            }
        }
    }
}
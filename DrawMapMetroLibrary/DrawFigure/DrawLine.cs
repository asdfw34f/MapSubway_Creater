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
        private Brush color1;
        private bool isMouseDown = false;
        private bool editLocation;
        private Canvas can;

        public DrawLine(Canvas canvas) 
        {
            can = canvas;
            Pstart1 = new Point(0, 0);
            Pend1 = new Point(0, 0);
        }

        /// <summary>
        /// Сводка:
        ///      Устанавливает истина или ложь для изменения 
        ///      локации линии маршрута по канвасу.
        /// </summary>
        /// <returns>
        ///      Возвращает текущее состаяние (изначально ложь).
        /// </returns>
        public bool iditLoc
        {
            get => editLocation;
            set => editLocation = value;
        }

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
            if (isMouseDown)
            {
                Line line = sender as Line;

                Canvas.SetLeft(line, e.GetPosition(can).X - line.X1);
                Canvas.SetTop(line, e.GetPosition(can).Y - line.Y1);
            }
        }

        private void NewLine_MouseLeftUp(object sender, MouseButtonEventArgs e)
        {
            if (!editLocation)
                return;

            Line line = sender as Line;
            line.ReleaseMouseCapture();
            isMouseDown = false;
        }

        private void NewLine_MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            if (!editLocation)
                return;

            Line line = sender as Line;
            isMouseDown = true;
            line.CaptureMouse();
        }
    }
}
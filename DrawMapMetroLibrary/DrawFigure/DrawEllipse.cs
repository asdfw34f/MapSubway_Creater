﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Model;
using System.Diagnostics;
using System.Net;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace EditorSubwayMap.DrawFigure
{
    public class DrawEllipse : IFigure
    {
        private Point pStart;
        private Point currentPoint;
        private Brush color1;
        private Canvas can;
        private bool isMouseDown = false;
        private bool editLocation = false;

        public DrawEllipse(Canvas canvas)
        {
            can = canvas;
            pStart = new Point(0, 0);
            currentPoint = new Point(0, 0);
        }

        /// <summary>
        /// Сводка:
        ///      Устанавливает истина или ложь для изменения локации круговой ветки метро.
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
        ///      Устанавливает начальные координаты позиции круговой ветки метро по канвасу.
        /// </summary>
        /// <returns>
        ///      Возвращает текущие начальные позицию круговой ветки метро по канвасу (изначально равен 0; 0).
        /// </returns>
        public Point Pstart
        {
            get => pStart;
            set => pStart = value;
        }

        /// <summary>
        /// Сводка:
        ///      Устанавливает конечные координаты позиции круговой ветки метро по канвасу.
        /// </summary>
        /// <returns>
        ///      Возвращает текущие конечную позицию круговой ветки метро по канвасу (изначально равен 0; 0).
        /// </returns>
        public Point Pend
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
                Width = 20,
                Height = 20,
                Margin = new Thickness(0),
                StrokeThickness = 5,
                Cursor= Cursors.Hand,
                Fill = Brushes.Transparent
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
            if (!editLocation)
                return;

            Ellipse b = sender as Ellipse;

            Canvas.SetLeft(b, e.GetPosition(can).X);
            Canvas.SetTop(b, e.GetPosition(can).Y);

            isMouseDown = true;
            b.CaptureMouse();
        }

        private void ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Ellipse b = sender as Ellipse;

                Canvas.SetLeft(b, Mouse.GetPosition(can).X);
                Canvas.SetTop(b, Mouse.GetPosition(can).Y);
            }
        }

        private void ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!editLocation)
                return;

            Ellipse b = sender as Ellipse;
            b.ReleaseMouseCapture();
            isMouseDown = false;
            editLocation = false;
        }
    }
}
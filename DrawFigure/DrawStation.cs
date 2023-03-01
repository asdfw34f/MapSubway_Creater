// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace EditorSubwayMap.DrawFigure
{
    internal class DrawStation : IFigure
    {
        private Point pStart;
        private Point currentPoint;
        private SolidColorBrush color1;
        private Canvas can;
        private bool isMouseDown = false;
        private bool editLocation = false;

        public DrawStation(Canvas canvas)
        {
            can = canvas;
            pStart = new Point(0, 0);
            currentPoint = new Point(0, 0);
            color1 = Brushes.Black;
        }

        public bool iditLoc
        {
            get => editLocation;
            set => editLocation = value;
        }

        public Point Pstart
        {
            get => pStart;
            set => pStart = value;
        }

        public Point Pend
        {
            get => currentPoint;
            set => currentPoint = value;
        }

        public SolidColorBrush color
        {
            get => color1;
            set => color1 = value;
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
                Margin = new Thickness(0)
            };

            Canvas.SetLeft(newSt, Pstart.X);
            Canvas.SetTop(newSt, Pstart.Y);

            newSt.MouseLeftButtonDown += station_MouseLeftButtonDown;
            newSt.MouseMove += station_MouseMove;
            newSt.MouseLeftButtonUp += station_MouseLeftButtonUp;
            newSt.AllowDrop = true;

            return newSt;
        }

        private void station_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!editLocation)
                return;

            Ellipse b = sender as Ellipse;

            Canvas.SetLeft(b, e.GetPosition(can).X);
            Canvas.SetTop(b, e.GetPosition(can).Y);

            isMouseDown = true;
            b.CaptureMouse();
        }

        private void station_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Ellipse b = sender as Ellipse;
                Canvas.SetLeft(b, Mouse.GetPosition(can).X);
                Canvas.SetTop(b, Mouse.GetPosition(can).Y);
            }
        }

        private void station_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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

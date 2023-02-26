using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Shapes;

namespace EditorSubwayMap.DrawFigure.EditLocation
{
    public class LocationEllipse
    {
        private Canvas can;
        double beginX = 0;
        double beginY = 0;
        bool isMouseDown = false;

        public LocationEllipse(Canvas canvas) 
        {
            can = canvas;
        }

        internal void ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse b = sender as Ellipse;
            beginX = e.GetPosition(can).X;
            beginY = e.GetPosition(can).Y;
            isMouseDown = true;
            b.CaptureMouse();
        }

        internal void ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Ellipse b = sender as Ellipse;
                double currX = e.GetPosition(can).X;
                double currY = e.GetPosition(can).Y;
                b.SetValue(Canvas.LeftProperty, currX);
                b.SetValue(Canvas.TopProperty, currY);
            }
        }

        internal void ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Ellipse b = sender as Ellipse;
            isMouseDown = false;
            b.ReleaseMouseCapture();
        }
    }
}

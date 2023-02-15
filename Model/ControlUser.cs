using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;


namespace EditorSubwayMap.Model
{
    public class ControlUser : MouseEventArgs
    {
        public ControlUser(MouseDevice mouse, int timestamp) : base(mouse, timestamp)
        {
            if (mouse != null)
            {

            }
        }
        public void ads(MouseEventArgs e, Canvas pic, Point px, Point py)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Pen pen = new Pen(Brushes.Black, 5);
                DrawingBrush brush = new DrawingBrush();
                Geometry geometry;
                geometry = new LineGeometry(px, py);
                Line line = new Line();
                GeometryDrawing geometryDrawing = new GeometryDrawing(brush, pen, geometry);
                geometryDrawing.Geometry = geometry;
                DrawingImage image = new DrawingImage();
                image.Drawing = geometryDrawing;
            }

        }
    }

}

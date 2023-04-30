using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace EditorSubwayMap.Models
{
    public class CanvasModel
    {
        private ObservableCollection<Element> _elements;

        public CanvasModel()
        {
            _elements = new ObservableCollection<Element>();
        }

        public ObservableCollection<Element> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        public void AddElement(Element element)
        {
            Elements.Add(element);
        }

        public void RemoveElement(Element element)
        {
            Elements.Remove(element);
        }

        public void UpdateElement(Element element)
        {
            int index = Elements.IndexOf(element);
            if (index != -1)
            {
                Elements[index] = element;
            }
        }
    }

    public abstract class Element
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class CircleMetro : Element
    {
        public double Radius { get; set; }
    }

    public class LineMetro : Element
    {
        public double X2 { get; set; }
        public double Y2 { get; set; }
    }

    public class StationMetro : Element
    {
    }
}
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;

namespace EditorSubwayMap.Models
{
    public class CanvasModel { }

    public class MyCanvas : INotifyPropertyChanged
    {
        #region Fields
        private Canvas _canvas;
        private bool _isDrawing;
        private Point _startPoint;
        private List<UIElement> _Children;
        private string _DrawMode;
        #endregion

        #region Propertyes
        public string DrawMode
        {
            get => _DrawMode;
            set
            {
                if (value == "Line" || value == "Circle" || value == "Station" || value == "")
                {
                    _DrawMode = value;
                    RaisePropertyChanged(nameof(DrawMode));
                }
            }
        }

        public List<UIElement> Children
        {
            get => _Children;
            set
            {
                _Children = value;
                RaisePropertyChanged(nameof(Children));
            }
        }

        public Point startPoint
        {
            get => _startPoint;
            set
            {
                _startPoint = value;
                RaisePropertyChanged("startPoint");
            }
        }

        public bool isDrawing
        {
            get => _isDrawing;
            set
            {
                _isDrawing = value;
                RaisePropertyChanged(nameof(isDrawing));
            }
        }

        public Canvas canvas
        {
            get => _canvas;
            set
            {
                if (_canvas != value)
                {
                    _canvas = value;
                    RaisePropertyChanged("canvas");
                }
            }
        }

        #endregion 

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
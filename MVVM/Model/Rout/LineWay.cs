using EditorSubwayMap.MVVM.Base;
using System.Collections.Generic;
using System.Windows;

namespace EditorSubwayMap.MVVM.Model.Rout
{
    public class LineWay : NotifyPropertyChanged
    {
        private string _NameWay;
        private string _Color;
        private Point _startPoint;
        private Point _endPoint;
        private List<Station> _stations;

        public string NameWay
        {
            get => _NameWay;
            set => Set(ref _NameWay, value);
        }

        public string Color
        {
            get => _Color;
            set => Set(ref _Color, value);
        }

        public Point startPoint
        {
            get => _startPoint;
            set => Set(ref _startPoint, value);
        }

        public Point endPoint
        {
            get => _endPoint;
            set => Set(ref _endPoint, value);
        }

        public List<Station> stations
        {
            get => _stations;
            set => Set(ref _stations, value);
        }

        public LineWay() { }
    }
}
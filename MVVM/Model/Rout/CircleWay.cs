using EditorSubwayMap.MVVM.Base;
using System.Collections.Generic;
using System.Windows;

namespace EditorSubwayMap.MVVM.Model.Rout
{
    public class CircleWay : NotifyPropertyChanged
    {
        private string _NameWay;
        private string _Color;
        private Point _Position;
        private double _Height;
        private double _Width;
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
        public Point Position
        {
            get => _Position;
            set => Set(ref _Position, value);
        }
        public double Height
        {
            get => _Height;
            set => Set(ref _Height, value);
        }
        public double Width
        {
            get => _Width;
            set => Set(ref _Width, value);
        }
        public List<Station> stations
        {
            get => _stations;
            set => Set(ref _stations, value);
        }
        public CircleWay() { }
    }
}
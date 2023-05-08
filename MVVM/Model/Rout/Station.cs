using EditorSubwayMap.MVVM.Base;
using System.Windows;

namespace EditorSubwayMap.MVVM.Model.Rout
{
    public class Station : NotifyPropertyChanged
    {
        private string _Name;
        private string _StationID;
        private int _distanceBack;
        private int _distanceLast;
        private string _NameWay;
        private string _Color;
        private Point _Position = new Point(0,0);

        public string StationID
        {
            get => _StationID;
            set => Set(ref _StationID, value);
        }
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }
        public int distanceLast
        {
            get => _distanceLast;
            set => Set(ref _distanceLast, value);
        }
        public int distanceBack
        {
            get => _distanceBack;
            set => Set(ref _distanceBack, value);
        }
        public string NameWay
        {
            get => _NameWay;
            set => Set(ref _NameWay, value);
        }
        public string Color
        {
            get => Color;
            set => Set(ref _Color, value);
        }
        public Point Position
        {
            get => _Position;
            set => Set(ref _Position, value);
        }
        public Station() { }
    }
}
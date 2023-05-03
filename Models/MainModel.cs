using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EditorSubwayMap.Models
{
    public class MainModel : Base.Model
    {
        Base.Model model;
        private struct Station
        {
            public string _NameStation;
            public string _distanceNext;
            public string _distanceBack;
        }

        private struct Way
        {
            public string NameWay;
        }

        private List<string> _WayList;

        public List<object> BrushList { get; } 


        #region Fields
        private Station _Station = new Station()
        {
            _NameStation = "Название станции",
            _distanceBack = "0",
            _distanceNext = "0"
        };
        private Way _Way = new Way
        {
            NameWay = "Название ветки"
        };
        #endregion


        public string NameStation
        {
            get => _Station._NameStation;
            set => Set(ref _Station._NameStation, value);
        }

        public string distanceNext
        {
            get => _Station._distanceNext;
            set => Set(ref _Station._distanceNext, value);
        }

        public string distanceBack
        {
            get => _Station._distanceBack;
            set => Set(ref _Station._distanceBack, value);
        }

        public string NameWay
        {
            get => _Way.NameWay;
            set => Set(ref _Way.NameWay, value);
        }

        public Array Brushes { get; }

        public MainModel()
        {
            BrushConverter conv = new BrushConverter();
            Brushes = typeof(Brushes).GetProperties().
                Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).
                ToArray();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Media;

namespace EditorSubwayMap.Models
{
    public class MainModel : Base.Model
    {
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


        #region Fields
        private List<string> _WayList = new List<string>();
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

        public MainModel()
        {
            var values = typeof(Brushes).GetProperties().
                 Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).
                 ToArray();
            Type Brushes =  values.GetType();
            
        }
    }
}
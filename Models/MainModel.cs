using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace EditorSubwayMap.Models
{
    public class MainModel : Base.Model
    {
        private struct TStationAtributs
        {
            public string _NameStation;
            public string _distanceNext;
            public string _distanceBack;
        }

        private struct TWayAtributs
        {
            public string NameWay;
        }

        #region Fields
        private List<string> _WayList = new List<string>();
        private TStationAtributs _StationAtributs = new TStationAtributs()
        {
            _NameStation = "Название станции",
            _distanceBack = "0",
            _distanceNext = "0"
        };
        private TWayAtributs _WayAtributs = new TWayAtributs
        {
            NameWay = "Название ветки"
        };
        private RouteSubway _RouteSubway = new RouteSubway();
        private int _IdStation = -1;
        #endregion

        #region Propertys
        /// <summary>
        /// Contain the Id last Station
        /// </summary>
        public int IdStation
        {
            get => _IdStation;
            set => Set(ref _IdStation, value);
        }
        /// <summary>
        /// List of Names of Ways, user saved
        /// </summary>
        public List<string> WayList 
        {
            get => _WayList;
            set => Set(ref _WayList, value);
        }
        /// <summary>
        /// Saved Stations end Ways
        /// </summary>
        public RouteSubway RouteSubway
        {
            get => _RouteSubway;
            set => Set(ref _RouteSubway, value);
        }
        /// <summary>
        /// Binding Textbox some Station atributs
        /// </summary>
        public string NameStation
        {
            get => _StationAtributs._NameStation;
            set => Set(ref _StationAtributs._NameStation, value);
        }
        /// <summary>
        /// Binding Textbox some Station atributs
        /// </summary>
        public string distanceNext
        {
            get => _StationAtributs._distanceNext;
            set => Set(ref _StationAtributs._distanceNext, value);
        }
        /// <summary>
        /// Binding Textbox some Station atributs
        /// </summary>
        public string distanceBack
        {
            get => _StationAtributs._distanceBack;
            set => Set(ref _StationAtributs._distanceBack, value);
        }
        /// <summary>
        /// Binding Textbox some Way atributs
        /// </summary>
        public string NameWay
        {
            get => _WayAtributs.NameWay;
            set => Set(ref _WayAtributs.NameWay, value);
        }
        #endregion

        public MainModel()
        {
            var values = typeof(Brushes).GetProperties().
                 Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).
                 ToArray();
            Type Brushes =  values.GetType();
            
        }
    }
}
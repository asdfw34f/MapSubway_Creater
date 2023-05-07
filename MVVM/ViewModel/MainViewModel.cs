using System;
using EditorSubwayMap.Infrastructure.Commands.MainCommands;
using EditorSubwayMap.Infrastructure.Commands;
using EditorSubwayMap.MVVM.Base;
using System.Windows.Input;
using EditorSubwayMap.Data;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using EditorSubwayMap.MVVM.Model.Rout;
using System.Windows;
using EditorSubwayMap.MVVM.Model;

namespace EditorSubwayMap.MVVM.ViewModel
{
    public class MainViewModel : NotifyPropertyChanged
    {
        /// <summary>
        /// public MainModel model = new MainModel();
        /// </summary>

        #region Commands
        public SelectModeDrawing SelectModeCommands { get; } = new SelectModeDrawing();
        public IOMap IOMapCommands { get; } = new IOMap();
        public Saving SavingCommands { get; } = new Saving();
        #endregion


        #region Fields
        private BrushConverter _ColorConvert = new BrushConverter();
        private List<string> _WayList = new List<string>() { "erwer", "sdfy", "Werwer" };
        private string _SelectedWay;
        private Brush _Color = Brushes.Black;
        private Array _Colors;

        private string _ID = "we";
        private string _NameStation = "Название станции: ";
        private string _distanceNext = "0";
        private string _distanceBack = "0";
        private Point _point = new Point(0, 0);
        private string _NameWay = "Название ветки :";

        private List<LineWay> _Lines= new List<LineWay>();
        private List<Station> _Stations = new List<Station>();
        private List<CircleWay> _Circles= new List<CircleWay>();

        #endregion
        #region Adding to Map Propertys
        public string NameWay
        {
            get => _NameWay;
            set => Set(ref _NameWay, value);
        }

        public string ID
        {
            get => _ID; set => Set(ref _ID, value);
        }

        public string NameStation
        {
            get => _NameStation; set => Set(ref _NameStation, value);
        }

        public string DistanceNext
        {
            get => _distanceNext; set => Set(ref _distanceNext, value);
        }

        public string DistanceBack
        {
            get => _distanceBack; set => Set(ref _distanceBack, value);
        }

        public Point Point
        {
            get => _point; set => Set(ref _point, value);
        }

        #endregion

        #region User tools
        public string SelectedWay { get => _SelectedWay; set=> Set(ref _SelectedWay, value); }
        public Array Colors {  get => _Colors; private set => Set(ref _Colors, value); }
        #endregion

        #region Map propertys 
        public Brush Color
        {
            get => _Color;
            set { if (Set(ref _Color, value)) { OnCanvas.Color = _Color; } }
        }
        #endregion

        


        public MainModel MainM;
        public MainViewModel()
        {
            MainM = new MainModel();
            
            Colors = typeof(Brushes).GetProperties()
            .Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).ToArray();

        }
    }
}
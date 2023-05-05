using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace EditorSubwayMap.Models
{
    public class MainModel : Base.Model
    {
        public struct StationAtributs
        {
            public static int ID = 0;
            public static string _NameStation = "Название станции";
            public static string _distanceNext = "0";
            public static string _distanceBack = "0";
        }

        public struct WayAtributs
        {
            public static string NameWay = "Название ветки";
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
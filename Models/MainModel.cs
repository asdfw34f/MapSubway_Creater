using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace EditorSubwayMap.Models
{
    public abstract class MainModel : Base.Model
    {
        public struct StationAtributs
        {
            public static int ID = 0;
            public static string _NameStation = "Название станции";
            public static string _distanceNext = "0";
            public static string _distanceBack = "0";
            public static Point _point = new Point(0, 0);
        }

        public struct WayAtributs
        {
            public static string NameWay = "Название ветки";
        }



        public MainModel()
        {
            
        }

    }
}
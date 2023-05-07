using System.Windows;
using EditorSubwayMap.MVVM.Base;

namespace EditorSubwayMap.MVVM.Model
{
    public class MainModel: NotifyPropertyChanged
    {   public CStationAtributs StationAtributs { get; set; }
        public CWayAtributs WayAtributs { get; set; }
        
        public MainModel()
        {
            StationAtributs = new CStationAtributs();
            WayAtributs= new CWayAtributs();
        }

        public class CStationAtributs : NotifyPropertyChanged
        {
            
        }

        public class CWayAtributs : NotifyPropertyChanged
        {
            
        }
    }
}
using EditorSubwayMap.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorSubwayMap.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        private string _Title;
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private string _DrawMode;
        public string DrawMode
        {
            get => _DrawMode;
            set => Set(ref _DrawMode, value);
        }
    }
}
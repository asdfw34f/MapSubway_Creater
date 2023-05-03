using EditorSubwayMap.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EditorSubwayMap.View
{
    /// <summary>
    /// Логика взаимодействия для CanvasView.xaml
    /// </summary>
    public partial class CanvasView : UserControl
    {
        public CanvasView()
        {
            InitializeComponent();
            DataContext = new CanvasViewModel(new MainViewModel());
        }
    }
}

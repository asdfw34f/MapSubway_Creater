// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using EditorSubwayMap.ViewModels;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _mainvm;

        public MainWindow()
        {
            InitializeComponent();
            _mainvm = new MainViewModel();
            DataContext = _mainvm;
        }

        private void CanDrawing_Loaded(object sender, RoutedEventArgs e)
        {
            CanvasViewControl.DataContext = _mainvm.canvasVM;
        }
    }
}
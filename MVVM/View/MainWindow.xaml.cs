// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using EditorSubwayMap.MVVM.ViewModel;
using System.Windows;
using System.Windows.Input;
namespace EditorSubwayMap.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            
            _vm = new MainViewModel();
            DataContext = _vm;
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _vm.CollapsedStation.Execute(null);
        }

        private void AtrWayGrid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _vm.CollapsedWay.Execute(null);
        }

        private void CanvasDrawing_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            _vm.MouseMove.Execute(sender);
        }

        private void CanvasDrawing_MouseMove(object sender, MouseEventArgs e)
        {
            _vm.MouseMove.Execute(sender);
        }

        private void CanvasDrawing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _vm.MouseDown.Execute(sender);
        }

        private void CanvasDrawing_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _vm.MouseUp.Execute(sender);
        }
    }
}
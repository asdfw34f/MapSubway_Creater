// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using EditorSubwayMap.View;
using EditorSubwayMap.ViewModels;
using System.Windows;

namespace WpfApp1
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
            /*
            
            */
            _vm = new MainViewModel();
            DataContext = _vm;
        }
    }
}
﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using EditorSubwayMap.View;
using EditorSubwayMap.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _model;
        public MainWindow()
        {
            InitializeComponent();
            /*
            < UserControl.Content >
                < views:CanvasView />
            </ UserControl.Content >
            */

            _model = new MainViewModel();
            DataContext = _model;
            CanvasDrawing.Content = new CanvasView(_model);
        }

        private void ComboColors_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBox ComboColor = sender as ComboBox;
            _model.SelectBrush.Execute((sender as ComboBox).SelectedValue as Brush);
        }
    }
}
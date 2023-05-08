﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using EditorSubwayMap.MVVM.View;
using EditorSubwayMap.MVVM.ViewModel;
using System.Windows;

namespace EditorSubwayMap.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _vm;
        CanvasViewModel _canvas;
        public MainWindow()
        {
            InitializeComponent();
            
            _vm = new MainViewModel();
            DataContext = _vm;
            _canvas = new CanvasViewModel(_vm);
            CanvasDrawing.Content = new CanvasView(_canvas);
            CanvasDrawing.DataContext = _canvas;
        }

    }
}
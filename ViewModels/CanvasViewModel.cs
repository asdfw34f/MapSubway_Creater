using EditorSubwayMap.Models;
using EditorSubwayMap.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp1;

namespace EditorSubwayMap.ViewModels
{
    public class CanvasViewModel : ViewModelBase
    {
        private ObservableCollection<Shape> _shapes;
        private ICommand _addCircleMetroCommand;
        private ICommand _addLineMetroCommand;
        private ICommand _addStationMetroCommand;

        public CanvasViewModel()
        {
            _shapes = new ObservableCollection<Shape>();
            _addCircleMetroCommand = new RelayCommand(AddCircleMetro);
            _addLineMetroCommand = new RelayCommand(AddLineMetro);
            _addStationMetroCommand = new RelayCommand(AddStationMetro);
        }

        public ObservableCollection<Shape> Shapes
        {
            get { return _shapes; }
            set
            {
                _shapes = value;
                NotifyPropertyChanged("Shapes");
            }
        }

        public ICommand AddCircleMetroCommand
        {
            get { return _addCircleMetroCommand; }
        }

        public ICommand AddLineMetroCommand
        {
            get { return _addLineMetroCommand; }
        }

        public ICommand AddStationMetroCommand
        {
            get { return _addStationMetroCommand; }
        }

        private void AddCircleMetro(object param)
        {
            Ellipse circleMetro = new Ellipse();
            circleMetro.Stroke = Brushes.Blue;
            circleMetro.Fill = Brushes.Transparent;
            circleMetro.Height = 100;
            circleMetro.Width = 100;
            Canvas.SetTop(circleMetro, 50);
            Canvas.SetLeft(circleMetro, 100);
            Shapes.Add(circleMetro);
        }

        private void AddLineMetro(object param)
        {
            Line lineMetro = new Line();
            lineMetro.Stroke = Brushes.Green;
            lineMetro.X1 = 100;
            lineMetro.Y1 = 200;
            lineMetro.X2 = 500;
            lineMetro.Y2 = 200;
            Shapes.Add(lineMetro);
        }

        private void AddStationMetro(object param)
        {
            Ellipse stationMetro = new Ellipse();
            stationMetro.Fill = Brushes.Red;
            stationMetro.Height = 10;
            stationMetro.Width = 10;
            Canvas.SetTop(stationMetro, 200);
            Canvas.SetLeft(stationMetro, 250);
            Shapes.Add(stationMetro);
        }
    }
}
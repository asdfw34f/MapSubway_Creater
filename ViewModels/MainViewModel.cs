using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Infastructure.Commands;
using EditorSubwayMap.Model;
using EditorSubwayMap.ViewModels.Base;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp1.Data;

namespace EditorSubwayMap.ViewModels
{
    public class MainViewModel : ViewModel
    {
        #region  X Y labels
        
        private string _labelX = "0";
        public string labelX
        {
            get => _labelX;
            set => Set(ref _labelX, value);
        }

        private string _labelY = "0";
        public string labelY
        {
            get => _labelY;
            set => Set(ref _labelY, value);
        }

        #endregion

        #region Canvas Property

        private List<UIElement> _Children;
        public List<UIElement> Children
        {
            get => _Children;
            set => Set(ref _Children, value);
        }

        private Canvas _Canvas = new Canvas();
        public Canvas Canvas
        {
            get => _Canvas;
            set => Set(ref _Canvas, value);
        }

        private bool _isDrawing;
        public bool isDrawing
        {
            get => _isDrawing;
            set => Set(ref _isDrawing, value);
        }

        private string _Color;
        public string Color
        {
            get => _Color;
            set => Set(ref _Color, value);
        }

        #endregion

        #region Add come station

        private string _NameStation;
        public string NameStation
        {
            get => _NameStation;
            set => Set(ref _NameStation, value);
        }

        private string _distanceNext = "0";
        public string distanceNext
        {
            get => _distanceNext;
            set => Set(ref _distanceNext, value);
        }

        private string _distanceBack = "0";
        public string distanceBack
        {
            get => _distanceBack;
            set => Set(ref _distanceBack, value);
        }

        #endregion

        #region Add come way
        
        private string _NameWay;
        public string NameWay
        {
            get => _NameWay;
            set => Set(ref _NameWay, value);
        }

        #endregion



        BrushConverter convColors;

        #region Select the drawing mode 

        public ICommand SelectDrawLineCommand{ get; }
        private bool CanSelectDrawLineCommand(object p) => true;
        private void OnSelectDrawLineCommand(object p) => DrawingOnCanvas.Drawing = DrawingOnCanvas.Modes.Line;

        public ICommand SelectDrawCircleCommand { get; }
        private bool CanSelectDrawCircleCommand(object p) => true;
        private void OnSelectDrawCircleCommand(object p) => DrawingOnCanvas.Drawing = DrawingOnCanvas.Modes.Circle;

        public ICommand SelectDrawStationCommand { get; }
        private bool CanSelectDrawStationCommand(object p) => true;
        private void OnSelectDrawStationCommand(object p) => DrawingOnCanvas.Drawing = DrawingOnCanvas.Modes.Station;

        public ICommand SelectDrawNoneCommand { get; }
        private bool CanSelectDrawNoneCommand(object p) => true;
        private void OnSelectDrawNoneCommand(object p) => DrawingOnCanvas.Drawing = DrawingOnCanvas.Modes.None;

        #endregion

        public MainViewModel()
        {
            //     <ContentPresenter Name="Cont" Content="{Binding Path=DrawingBoard.DrawingCanvas}">
            #region Select the drawing mode 
            SelectDrawLineCommand = new LambdaCommand(OnSelectDrawLineCommand, CanSelectDrawLineCommand);
            SelectDrawCircleCommand = new LambdaCommand(OnSelectDrawCircleCommand, CanSelectDrawCircleCommand);
            SelectDrawStationCommand = new LambdaCommand(OnSelectDrawStationCommand, CanSelectDrawStationCommand);
            SelectDrawNoneCommand = new LambdaCommand(OnSelectDrawNoneCommand, CanSelectDrawNoneCommand);
            #endregion
            
        }
    }
}
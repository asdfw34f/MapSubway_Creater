using System.Windows.Controls;
using System.Windows.Input;
using EditorSubwayMap.MVVM.ViewModel;

namespace EditorSubwayMap.MVVM.View
{
    public partial class CanvasView : UserControl
    {
        private CanvasViewModel _viewModel;
        public CanvasView(CanvasViewModel ViewModel)
        {
            InitializeComponent();
            _viewModel = ViewModel;
            
        }
        
        private void DrawingCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            _viewModel.MouseMove.Execute(sender);
        }

        private void DrawingCanvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.MouseDown.Execute(sender);
        }

        private void DrawingCanvas_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _viewModel.MouseUp.Execute(sender);
        }
    }
}
using System.Windows.Controls;
using System.Windows.Input;
using EditorSubwayMap.ViewModels;

namespace EditorSubwayMap.View
{
    public partial class CanvasView : UserControl
    {
        private CanvasViewModel _viewModel;
        public CanvasView()
        {
            InitializeComponent();
            _viewModel = new CanvasViewModel();
        }

        private void DrawingCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            _viewModel.MouseMove.Execute(sender);
        }

        private void DrawingCanvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void DrawingCanvas_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
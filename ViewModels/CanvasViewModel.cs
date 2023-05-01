using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EditorSubwayMap.ViewModels
{
    internal class CanvasViewModel
    {
        public Point MousePosition { get; set; }

        public void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var canvas = sender as Canvas;
            var viewModel = canvas.DataContext as CanvasViewModel;
            viewModel.MousePosition = e.GetPosition(canvas);
        }
    }
}

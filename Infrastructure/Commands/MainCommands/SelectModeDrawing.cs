using EditorSubwayMap.Data;
using EditorSubwayMap.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EditorSubwayMap.Infrastructure.Commands.MainCommands
{
    public class SelectModeDrawing
    {
        public ICommand SelectDrawLineCommand { get; }
        private bool CanSelectDrawLineCommand(object p) => true;
        private void OnSelectDrawLineCommand(object p) => OnCanvas.Drawing = OnCanvas.Modes.Line;

        public ICommand SelectDrawCircleCommand { get; }
        private bool CanSelectDrawCircleCommand(object p) => true;
        private void OnSelectDrawCircleCommand(object p) => OnCanvas.Drawing = OnCanvas.Modes.Circle;

        public ICommand SelectDrawStationCommand { get; }
        private bool CanSelectDrawStationCommand(object p) => true;
        private void OnSelectDrawStationCommand(object p) => OnCanvas.Drawing = OnCanvas.Modes.Station;

        public ICommand SelectDrawNoneCommand { get; }
        private bool CanSelectDrawNoneCommand(object p) => true;
        private void OnSelectDrawNoneCommand(object p) => OnCanvas.Drawing = OnCanvas.Modes.None;

        public SelectModeDrawing() 
        {
            SelectDrawLineCommand = new LambdaCommand(OnSelectDrawLineCommand, CanSelectDrawLineCommand);
            SelectDrawCircleCommand = new LambdaCommand(OnSelectDrawCircleCommand, CanSelectDrawCircleCommand);
            SelectDrawStationCommand = new LambdaCommand(OnSelectDrawStationCommand, CanSelectDrawStationCommand);
            SelectDrawNoneCommand = new LambdaCommand(OnSelectDrawNoneCommand, CanSelectDrawNoneCommand);
        }
    }
}

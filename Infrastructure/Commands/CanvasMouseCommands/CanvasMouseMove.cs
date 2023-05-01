using EditorSubwayMap.Data;
using EditorSubwayMap.DrawFigure;
using EditorSubwayMap.Infastructure.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;

namespace EditorSubwayMap.Infrastructure.Commands.CanvasMouseCommands
{
    internal class CanvasMouseMove : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            
        }
    }
}
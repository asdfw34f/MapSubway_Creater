using EditorSubwayMap.Infastructure.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorSubwayMap.Infrastructure.Commands.CanvasMouseCommands
{
    internal class CanvasMouseUp : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {

        }
    }
}

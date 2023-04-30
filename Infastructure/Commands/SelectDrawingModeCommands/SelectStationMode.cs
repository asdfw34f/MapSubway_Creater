﻿using EditorSubwayMap.Data;
using EditorSubwayMap.Infastructure.Commands.Base;

namespace EditorSubwayMap.Infastructure.Commands.SelectDrawingModeCommands
{
    internal class SelectStationMode : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => DrawingMode.DrawMode = "Station";
    }
}
using System;
using EditorSubwayMap.Infastructure.Commands;
using EditorSubwayMap.ViewModels.Base;
using System.Windows.Input;
using WpfApp1.Data;

namespace EditorSubwayMap.ViewModels
{
    public class MainViewModel : ViewModel
    {


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

        #region Map Commands

        public ICommand Save { get; }
        private bool CanSaved(Object p) => true;

        private void SaveExecute(Object p)
        {
            
        }

        public ICommand Import { get; }
        private bool CanImport(Object p) => true;

        private void ImportExecute(Object p)
        {
            
        }

        public ICommand RemoveAll { get; }
        private bool CanRemoveAll(Object p) => true;

        private void OnRemoveAllExecute(Object p)
        {
            
        }
        #endregion

        #region 

        #endregion

        public MainViewModel()
        {
            #region Map Commands
            Import = new LambdaCommand(ImportExecute, CanImport);
            Save = new LambdaCommand(SaveExecute, CanSaved);
            #endregion            
            
            #region Select the drawing mode 
            SelectDrawLineCommand = new LambdaCommand(OnSelectDrawLineCommand, CanSelectDrawLineCommand);
            SelectDrawCircleCommand = new LambdaCommand(OnSelectDrawCircleCommand, CanSelectDrawCircleCommand);
            SelectDrawStationCommand = new LambdaCommand(OnSelectDrawStationCommand, CanSelectDrawStationCommand);
            SelectDrawNoneCommand = new LambdaCommand(OnSelectDrawNoneCommand, CanSelectDrawNoneCommand);
            #endregion
            
        }
    }
}
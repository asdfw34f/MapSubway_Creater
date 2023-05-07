using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EditorSubwayMap.Infrastructure.Commands.MainCommands
{
    public class IOMap
    {
        public ICommand Save { get; }
        private bool CanSaved(object p) => true;
        private void OnSaveExecute(object p)
        {

        }

        public ICommand Import { get; }
        private bool CanImport(object p) => true;
        private void OnImportExecute(object p)
        {

        }

        public ICommand RemoveAll { get; }
        private bool CanRemoveAll(object p) => true;
        private void OnRemoveAllExecute(object p)
        {

        }

        public IOMap() 
        {
            Import = new LambdaCommand(OnImportExecute, CanImport);
            Save = new LambdaCommand(OnSaveExecute, CanSaved);
            RemoveAll = new LambdaCommand(OnRemoveAllExecute, CanRemoveAll);
        }
    }
}
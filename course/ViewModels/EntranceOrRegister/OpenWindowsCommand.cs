using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace course.ViewModels.EntranceOrRegister
{
    class OpenWindowsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _execute;
        public OpenWindowsCommand(Action execute)
        {
            _execute = execute;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}

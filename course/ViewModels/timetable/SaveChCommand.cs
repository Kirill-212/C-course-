using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course.ViewModels.timetable
{
    class SaveChCommand : System.Windows.Input.ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _execute;
        public SaveChCommand(Action execute)
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

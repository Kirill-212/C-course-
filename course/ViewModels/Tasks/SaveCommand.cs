﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace course.ViewModels.Tasks
{
    class SaveCommand: ICommand
    {
        private Action _execute;
        public SaveCommand(Action execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

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

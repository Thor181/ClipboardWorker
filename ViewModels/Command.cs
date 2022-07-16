using ClipboardWorker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClipboardWorker.ViewModels
{
    internal class Command : ICommand
    {
        public Action<object> action;
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter != null)
            {
                this.action.Invoke(parameter);
            }
        }

        public Command(Action<object> action)
        {
            this.action = action;
        }
    }
}

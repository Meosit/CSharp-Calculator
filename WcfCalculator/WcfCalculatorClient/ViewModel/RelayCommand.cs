using System;
using System.Windows.Input;

namespace WcfCalculatorClient.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly CanExecuteReference _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action execute, CanExecuteReference canExecute)
        {
            this._execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Value;
        }

        public void Execute(object parameter)
        {
            this._execute();
        }
    }


    public class CanExecuteReference
    {
        public bool Value { get; set; }
    }
}

using System;
using System.Windows.Input;

namespace WcfCalculatorClient.ViewModel
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> execute)
        {
            this._execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this._execute((T)parameter);
        }
    }
    
}

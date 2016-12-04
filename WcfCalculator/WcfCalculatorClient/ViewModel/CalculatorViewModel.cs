
using System.Windows.Input;

namespace WcfCalculatorClient.ViewModel
{
    class CalculatorViewModel : BaseViewModel
    {
        private CanExecuteReference _isUiAvailable = new CanExecuteReference() {Value = true};
    }

}
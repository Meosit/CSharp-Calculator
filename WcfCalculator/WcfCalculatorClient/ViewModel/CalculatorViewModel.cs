using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media;
using WcfCalculatorClient.Model;

namespace WcfCalculatorClient.ViewModel
{
    public class CalculatorViewModel : BaseViewModel
    {
        private readonly Regex _inputExpressionRegex = new Regex(@"^-?(√?\d+(\.\d+)?)([+\-*/](√?\d+(\.\d+)?))*$");
        private readonly CalculatorModel _model;

        public CalculatorViewModel()
        {
            CalculateCommand = new RelayCommand<string>(param =>
                        OutputText = IsValidInput(InputText) ? _model.Calculate(InputText) : "Invalid Input"
            );
            InputCommand = new RelayCommand<string>(input =>
            {
                InputText += input;
                InputTextBackground = IsValidInput(InputText) ? Brushes.White : Brushes.Crimson;
            });
            EarseCommand = new RelayCommand<string>(param => InputText = "");
            BackspaceCommand = new RelayCommand<string>(param =>
            {
                if (InputText.Length > 0)
                {
                    InputText = InputText.Remove(InputText.Length - 1);
                }
                InputTextBackground = IsValidInput(InputText) ? Brushes.White : Brushes.Crimson;
            });
            _model = new CalculatorModel();
            WindowCloseCommand = new RelayCommand<object>(o => _model.CloseService());
        }


        public ICommand InputCommand { get; }
        public ICommand CalculateCommand { get; }
        public ICommand EarseCommand { get; }
        public ICommand BackspaceCommand { get; }
        public ICommand WindowCloseCommand { get; }

        private string _inputText = "";
        private Brush _inputTextBackground = Brushes.White;
        private string _outputText = "";

        public string InputText
        {
            get { return _inputText; }
            set
            {
                _inputText = value;
                OnPropertyChanged();
            }
        }

        public string OutputText
        {
            get { return _outputText; }
            set
            {
                _outputText = value;
                OnPropertyChanged();
            }
        }


        public Brush InputTextBackground
        {
            get { return _inputTextBackground; }
            set
            {
                _inputTextBackground = value;
                OnPropertyChanged();
            }
        }

        private bool IsValidInput(string input)
        {
            return _inputExpressionRegex.IsMatch(input);
        }
    }
}
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media;
using WcfCalculatorClient.Model;

namespace WcfCalculatorClient.ViewModel
{
    public class CalculatorViewModel : BaseViewModel
    {
        private readonly Regex _inputExpressionRegex = new Regex(@"^-?(√?\d+(,\d+)?)([+\-*/](√?\d+(,\d+)?))*$");
        private readonly CalculatorModel _model;

        public CalculatorViewModel()
        {
            CalculateCommand = new RelayCommand<string>(param =>
                        OutputText = IsValidInput(InputText) ? _model.Calculate(param) : "Invalid Input"
            );
            InputCommand = new RelayCommand<string>(input =>
            {
                InputText += input;
                InputTextBackground = IsValidInput(InputText) ? Brushes.White : Brushes.Crimson;
            });
            EarseCommand = new RelayCommand<string>(param => OutputText = "");
            BackspaceCommand = new RelayCommand<string>(param =>
            {
                if (OutputText.Length != 0)
                {
                    OutputText = OutputText.Remove(OutputText.Length - 1);
                }
            });
            _model = new CalculatorModel();
        }


        public ICommand InputCommand { get; }
        public ICommand CalculateCommand { get; }


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

        public ICommand EarseCommand { get; }
        public ICommand BackspaceCommand { get; }

        private bool IsValidInput(string input)
        {
            return _inputExpressionRegex.IsMatch(input);
        }
    }
}
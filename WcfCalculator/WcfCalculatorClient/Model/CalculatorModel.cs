using System;
using System.ServiceModel;
using System.Text.RegularExpressions;
using WcfCalculator;

namespace WcfCalculatorClient.Model
{
    class CalculatorModel
    {
        private readonly ICalculator _calculatorService;

        public CalculatorModel()
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:8000/");
            var channelFactory = new ChannelFactory<ICalculator>(binding, endpoint);
            _calculatorService = channelFactory.CreateChannel();
        }

        public string Calculate(string expr)
        {
            try
            {
                if (expr[0] == '-')
                {
                    expr = "0" + expr;
                }
                Match match = Regex.Match(expr, @"^([\-+*/]|√?[^\-+*/]+)+$");
                double result;
                if (match.Groups.Count == 1)
                {
                    result = CalculateUnaryOperator(match.Groups[0].Value);
                }
                else
                {
                    double a = Double.Parse(match.Groups[1].Value);
                    double b = Double.Parse(match.Groups[3].Value);
                    result = CalculateBinaryOperator(a, b, match.Groups[2].Value);
                    for (int i = 4; i < match.Groups.Count - 1; i+= 2)
                    {
                        b = Double.Parse(match.Groups[i + 1].Value);
                        result = CalculateBinaryOperator(result, b, match.Groups[i].Value);
                    }
                }

                return result.ToString();
            }
            catch (TimeoutException)
            {
                return "Execution timeout error.";
            }
            catch (FaultException<MathFault> mathFault)
            {
                return mathFault.Detail.Operation + " error. " + mathFault.Detail.ProblemType;
            }
        }

        private double CalculateBinaryOperator(double a, double b, string operatorSign)
        {
            double result;
            switch (operatorSign)
            {
                case "*":
                    result = _calculatorService.Multiply(a, b);
                    break;
                case "/":
                    result = _calculatorService.Divide(a, b);
                    break;
                case "-":
                    result = _calculatorService.Substract(a, b);
                    break;
                case "+":
                    result = _calculatorService.Add(a, b);
                    break;
                default:
                    result = Double.NaN;
                    break;
            }
            return result;
        }

        private double CalculateUnaryOperator(string operand)
        {
            double result;
            if (operand[0] == '√')
            {
                double a = Double.Parse(operand.Remove(0, 1));
                result = _calculatorService.Sqrt(a);
            }
            else
            {
                result = Double.Parse(operand);
            }
            return result;
        }

        public void CloseService()
        {
            var communicationObject = (ICommunicationObject) _calculatorService;
            communicationObject?.Close();
        }
    }
}
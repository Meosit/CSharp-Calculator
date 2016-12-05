using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel;
using System.Text.RegularExpressions;
using WcfCalculatorClient.CalculatorServiceReference;

namespace WcfCalculatorClient.Model
{
    class CalculatorModel
    {
        private readonly Regex _expressionUnitRegex = new Regex(@"[\-+*/]|√?[^\-+√*/]+");
        private readonly Dictionary<string, Func<double, double, double>> _serviceFunctions;
        private readonly MySuperCalculatorClient _client;

        public CalculatorModel()
        {
            
            _client = new MySuperCalculatorClient();
            _serviceFunctions = new Dictionary<string, Func<double, double, double>>()
            {
                {"+", ((a, b) => _client.Add(a, b))},
                {"-", ((a, b) => _client.Substract(a, b))},
                {"*", ((a, b) => _client.Multiply(a, b))},
                {"/", ((a, b) => _client.Divide(a, b))}
            };
        }

        public string Calculate(string expr)
        {
            try
            {
                if (expr[0] == '-')
                {
                    expr = "0" + expr;
                }
                MatchCollection matches = _expressionUnitRegex.Matches(expr);
                double result;
                if (matches.Count == 1)
                {
                    result = CalculateUnaryOperator(matches[0].Value);
                }
                else
                {
                    double a = CalculateUnaryOperator(matches[0].Value);
                    double b = CalculateUnaryOperator(matches[2].Value);
                    result = CalculateBinaryOperator(a, b, matches[1].Value);
                    for (int i = 3; i < matches.Count - 1; i += 2)
                    {
                        b = CalculateUnaryOperator(matches[i + 1].Value);
                        result = CalculateBinaryOperator(result, b, matches[i].Value);
                    }
                }

                return result.ToString(CultureInfo.InvariantCulture);
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
            return _serviceFunctions[operatorSign].Invoke(a, b);
        }

        private double CalculateUnaryOperator(string operand)
        {
            double result;
            if (operand[0] == '√')
            {
                double a = Double.Parse(operand.Remove(0, 1));
                result = _client.Sqrt(a);
            }
            else
            {
                result = Double.Parse(operand);
            }
            return result;
        }

        public void CloseService()
        {
            _client?.Close();
        }
    }
}
using System;
using System.ServiceModel;

namespace WcfCalculator
{
    public class Calculator : ICalculator
    {
        public double Add(double a, double b)
        {
            if (double.IsNaN(a) || double.IsNaN(b))
            {
                MathFault fault = new MathFault()
                {
                    Operation = "Add",
                    ProblemType = "NaN value passed. Parameters must be double."
                };
                throw new FaultException<MathFault>(fault);
            }
            try
            {
                return a + b;
            }
            catch (OverflowException)
            {
                MathFault fault = new MathFault()
                {
                    Operation = "Add",
                    ProblemType = "Type overflow."
                };
                throw new FaultException<MathFault>(fault);
            }
        }

        public double Substract(double a, double b)
        {
            if (double.IsNaN(a) || double.IsNaN(b))
            {
                MathFault fault = new MathFault()
                {
                    Operation = "Substract",
                    ProblemType = "NaN value passed. Parameters must be double."
                };
                throw new FaultException<MathFault>(fault);
            }
            try
            {
                return a - b;
            }
            catch (OverflowException)
            {
                MathFault fault = new MathFault()
                {
                    Operation = "Substract",
                    ProblemType = "Type overflow."
                };
                throw new FaultException<MathFault>(fault);
            }
        }

        public double Multiply(double a, double b)
        {
            if (double.IsNaN(a) || double.IsNaN(b))
            {
                MathFault fault = new MathFault()
                {
                    Operation = "Multiply",
                    ProblemType = "NaN value passed. Parameters must be double."
                };
                throw new FaultException<MathFault>(fault);
            }
            try
            {
                return a*b;
            }
            catch (OverflowException)
            {
                MathFault fault = new MathFault()
                {
                    Operation = "Multiply",
                    ProblemType = "Type overflow."
                };
                throw new FaultException<MathFault>(fault);
            }
        }

        public double Divide(double a, double b)
        {
            if (double.IsNaN(a) || double.IsNaN(b))
            {
                MathFault fault = new MathFault()
                {
                    Operation = "Multiply",
                    ProblemType = "NaN value passed. Parameters must be double."
                };
                throw new FaultException<MathFault>(fault);
            }
            if (b == 0)
            {
                MathFault fault = new MathFault()
                {
                    Operation = "Divide",
                    ProblemType = "Cannot divide by zero."
                };
                throw new FaultException<MathFault>(fault);
            }
            return a/b;
        }

        public double Sqrt(double a)
        {
            if (double.IsNaN(a))
            {
                MathFault fault = new MathFault()
                {
                    Operation = "Sqrt",
                    ProblemType = "NaN value passed. Parameter must be non-negative double."
                };
                throw new FaultException<MathFault>(fault);
            }
            if (double.IsPositiveInfinity(a))
            {
                MathFault fault = new MathFault()
                {
                    Operation = "Sqrt",
                    ProblemType = "Infinity passed. Parameter must be non-negative double."
                };
                throw new FaultException<MathFault>(fault);
            }
            if (a < 0)
            {
                MathFault fault = new MathFault()
                {
                    Operation = "Sqrt",
                    ProblemType = "Negative value passed. Parameter must be non-negative double."
                };
                throw new FaultException<MathFault>(fault);
            }
            return Math.Sqrt(a);
        }
    }
}
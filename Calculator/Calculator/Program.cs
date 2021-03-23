using System;

namespace Calculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            double initialValue = 0.0;

            Calculator calculator = new Calculator(initialValue);

            //Leaving local variables for debugging purposes
            var additionResult = calculator.Add(20);
            var substracttionResult = calculator.Subtract(10);
            var multiplicationResult = calculator.Multiply(25);
            var divisionResult = calculator.Divide(2.5);

            Console.WriteLine($"Final Result of calculation: {calculator.CurrentValue}");
        }
    }

    //An alternative to this could be to create Calculator extension methods. This would allow us to chain them all together but might look a little less readable and makes debugging harder...
    public class Calculator
    {
        public double CurrentValue { get; private set; }

        public Calculator(double initialValue = 0)
        {
            CurrentValue = initialValue;
        }

        public double Add(double numToAdd)
        {
            CurrentValue += numToAdd;
            return CurrentValue;
        }

        public double Subtract(double numToSubtract)
        {
            CurrentValue -= numToSubtract;
            return CurrentValue;
        }

        public double Multiply(double numToMultiply)
        {
            CurrentValue *= numToMultiply;
            return CurrentValue;
        }

        public double Divide(double numToDivide)
        {
            if (numToDivide == 0)
            {
                throw new InvalidOperationException($"Cannot divide by non-zero value. Value: {numToDivide}");
            }
            
            CurrentValue /= numToDivide;
            return CurrentValue;
        }

        //***Method Violates Single Responsibility Principle***
        //public double Calculate(double b)
        //{
        //    if (calculationType.Equals("add"))
        //    {
        //        CalculationAdd calculationAdd = new CalculationAdd();
        //        calculationAdd.a = a;
        //        calculationAdd.b = b;
        //        return calculationAdd.Calculate();
        //    }

        //    if (calculationType.Equals("subtract"))
        //    {
        //        CalculationSubtract calculationSubtract = new CalculationSubtract();
        //        calculationSubtract.a = a;
        //        calculationSubtract.b = b;
        //        return calculationSubtract.Calculate();
        //    }

        //    if (calculationType.Equals("multiply"))
        //    {
        //        CalculationMultiply calculationMultiply = new CalculationMultiply();
        //        calculationMultiply.a = a;
        //        calculationMultiply.b = b;
        //        return calculationMultiply.Calculate();
        //    }

        //    if (calculationType.Equals("divide"))
        //    {
        //        CalculationDivide calculationDivide = new CalculationDivide();
        //        calculationDivide.a = a;
        //        calculationDivide.b = b;
        //        return calculationDivide.Calculate();
        //    }
        //}
    }

    /*
    The following classes do not add value as they simply add another layer of abstraction. 
    There is no reason to have this extra layer when the calculations can be performed directly in the calculator class without violating any SOLID principles

    If I were to use this class, I would make the local variables private and rename them to something that is more descriptive. 
    These private variables would be set in the constructor instead of directly setting them outside of the class

    ex.

        public class CalculationDivide
    {
        private readonly double numerator;
        private readonly double denominator;
        
        public CalculationAdd(double numerator, double denominator) {
            
            if (denominator == 0)
            {
                throw new InvalidOperationException($"Cannot divide by non-zero value. Value: {denominator}");
            }
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public double Calculate()
        {
            return numerator / denominator;
        }
    }


    */

    //public class CalculationAdd
    //{
    //    public double a;
    //    public double b;

    //    public double Calculate()
    //    {
    //        return a + b;
    //    }
    //}

    //public class CalculationSubtract
    //{
    //    public double a;
    //    public double b;

    //    public double Calculate()
    //    {
    //        return a - b;
    //    }
    //}

    //public class CalculationMultiply
    //{
    //    public double a;
    //    public double b;

    //    public double Calculate()
    //    {
    //        return a * b;
    //    }
    //}

    //public class CalculationDivide
    //{
    //    public double a;
    //    public double b;

    //    public double Calculate()
    //    {
    //        return a / b;
    //    }
    //}
}

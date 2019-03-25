using System;

namespace Calculator
{
    /// <summary>
    /// Diagnostic class which oputputs the calculation results to the console.
    /// </summary>
    public class Diagnostics : IDiagnostics
    {
        
        public void AddToConsole(int numberOne, int numberTwo, int result)
        {
            Console.WriteLine("The addition result of " + numberOne  + " + " + numberTwo + " = " + result);
        }

        public void SubtractToConsole(int numberOne, int numberTwo, int result)
        {
            Console.WriteLine("The subtraction result of " +numberOne+ " - " + numberTwo + " = " + result);
        }

        public void MultiplyToConsole(int numberOne, int numberTwo, int result)
        {
            Console.WriteLine("The multiplication result of " + numberOne + " - " + numberTwo +" = " + result);
        }

        public void DivideToConsole(int numberOne, int numberTwo, int result)
        {
            Console.WriteLine("The division result of " + numberOne + " - " + numberTwo + " = " + result);
        }
    }
}

namespace Calculator
{
    /// <summary>
    /// Implements the 4 Calculator Operations and sends the oputput to the relevant diagnostics interface
    /// </summary>
    public class SimpleCalculator :ISimpleCalculator
    {
        private readonly IDiagnostics _calculatorDiagnostics;
        private readonly IDummyDiagnostics _dummyDiagnostics;
        private int _result;
        
        public SimpleCalculator(IDiagnostics calculatorDiagnostics, IDummyDiagnostics dummyDiagnostics)
        {
            _calculatorDiagnostics = calculatorDiagnostics;
            _dummyDiagnostics = dummyDiagnostics;
        }
        
       public int Add(int numberOne, int numberTwo)
        {
            _result = numberOne + numberTwo;
            _calculatorDiagnostics.AddToConsole(numberOne, numberTwo, _result);
            _dummyDiagnostics.DoAdd();
            return _result;
        }

        public int Subtract(int numberOne, int numberTwo)
        {
            _result = numberOne - numberTwo;
            _calculatorDiagnostics.SubtractToConsole(numberOne, numberTwo, _result);
            _dummyDiagnostics.DoSubtract();
            return _result;
        }

        public int Multiply(int numberOne, int numberTwo)
        {
            _result = numberOne * numberTwo;
            _calculatorDiagnostics.MultiplyToConsole(numberOne, numberTwo, _result);
            _dummyDiagnostics.DoMultiply();
            return _result;
        }

        public int Divide(int numberOne, int numberTwo)
        {
            _result = numberTwo == 0 ? 0 : numberOne/numberTwo;
            _calculatorDiagnostics.DivideToConsole(numberOne, numberTwo, _result);
            _dummyDiagnostics.DoDivide();
            return _result;
        }

      
    }
}

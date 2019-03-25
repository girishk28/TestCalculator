namespace Calculator
{
    public interface  IDiagnostics
    {
        void AddToConsole(int numberOne, int numberTwo, int result);
        void SubtractToConsole(int numberOne, int numberTwo, int result);
        void MultiplyToConsole(int numberOne, int numberTwo, int result);
        void DivideToConsole(int numberOne, int numberTwo, int result);
    }
}

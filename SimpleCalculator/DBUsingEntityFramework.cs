using Repository;

namespace Calculator
{
    /// <summary>
    /// Saves the diagnostics to the database using Entity Framework
    /// </summary>
    public class DbUsingEntityFramework : IDiagnostics
    {
        private const string SOURCE = " (Inserted using Entity Framework ORM Layer)";
        private string _databaseDiagnostics;
        private readonly ICalculatorRepository _calculatorRepository;

        public DbUsingEntityFramework(ICalculatorRepository calculatorRepository)
        {
            _calculatorRepository = calculatorRepository;
        }

        public void AddToConsole(int numberOne, int numberTwo, int result)
        {
            _databaseDiagnostics = "The addition result of " + numberOne + " + " + numberTwo + " = " + result;
            AddToDatabaseUsingEntityFramework(_databaseDiagnostics + SOURCE);
        }

        public void SubtractToConsole(int numberOne, int numberTwo,int result)
        {
            _databaseDiagnostics = "The subtraction result of " + numberOne + " - " + numberTwo + " = " + result;
            AddToDatabaseUsingEntityFramework(_databaseDiagnostics + SOURCE);
        }

        public void MultiplyToConsole(int numberOne, int numberTwo,int result)
        {
            _databaseDiagnostics = "The multiplication result of " + numberOne + " * " + numberTwo +
                                   " = " + result;
            AddToDatabaseUsingEntityFramework(_databaseDiagnostics + SOURCE);
        }

        public void DivideToConsole(int numberOne, int numberTwo,int result)
        {
            _databaseDiagnostics = "The division result of " + numberOne + " / " + numberTwo + " = " + result;
            AddToDatabaseUsingEntityFramework(_databaseDiagnostics + SOURCE);
        }

        public void AddToDatabaseUsingEntityFramework(string result)
        {
            _calculatorRepository.AddToDatabase(result);
        }
    }
}

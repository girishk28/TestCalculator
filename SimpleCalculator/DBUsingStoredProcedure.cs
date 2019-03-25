using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Repository;

namespace Calculator
{
    /// <summary>
    /// Saves the diagnostics to the database using Stored Procedure
    /// </summary>
    public class DbUsingStoredProcedure: IDiagnostics
    {
        private const string SOURCE = " (Inserted using Stored Procedure Data Layer)";
        private string _databaseDiagnostics;
        private readonly ICalculatorRepository _calculatorRepository;

        public DbUsingStoredProcedure(ICalculatorRepository calculatorRepository)
        {
            _calculatorRepository = calculatorRepository;
        }

        public void AddToConsole(int numberOne, int numberTwo, int result)
        {
            _databaseDiagnostics = "The addition result of " + numberOne  + " + " + numberTwo + " = " + result;
            AddToDatabaseUsingStoredProcedure(_databaseDiagnostics + SOURCE);
        }

        public void SubtractToConsole(int numberOne, int numberTwo, int result)
        {
            _databaseDiagnostics ="The subtraction result of " +numberOne+ " - " + numberTwo + " = " + result;
            AddToDatabaseUsingStoredProcedure(_databaseDiagnostics + SOURCE);
        }

        public void MultiplyToConsole(int numberOne, int numberTwo, int result)
        {
            _databaseDiagnostics = "The multiplication result of " + numberOne + " - " + numberTwo +
                                   " = " + result;
            AddToDatabaseUsingStoredProcedure(_databaseDiagnostics + SOURCE);
        }

        public void DivideToConsole(int numberOne, int numberTwo, int result)
        {
            _databaseDiagnostics = "The division result of " + numberOne + " - " + numberTwo + " = " + result;
            AddToDatabaseUsingStoredProcedure(_databaseDiagnostics + SOURCE);
        }

        public void AddToDatabaseUsingStoredProcedure(string result)
        {
            _calculatorRepository.AddToDatabase(result);
        }

       
    }
}

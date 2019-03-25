using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Repository
{
    /// <summary>
    /// Implements ICalculatorRepository interface for Stored Procedure
    /// </summary>
    public class CalculatorRepositoryStoredProcedure : ICalculatorRepository
    {
        public void AddToDatabase(string result)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ResultsConnection"].ConnectionString;

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("dbo.uspAddResult", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter resultParameter = command.Parameters.Add("@Result", SqlDbType.NVarChar, 100);
            resultParameter.Direction = ParameterDirection.Input;
            resultParameter.Value = result;

            sqlConnection.Open();

            int rowsAffected = command.ExecuteNonQuery();

            sqlConnection.Close();
        }
    }
}

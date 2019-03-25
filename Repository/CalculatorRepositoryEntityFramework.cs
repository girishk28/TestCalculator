using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// Implements ICalculatorRepository interface for Entity Framework
    /// </summary>
    public class CalculatorRepositoryEntityFramework : ICalculatorRepository
    {
        public void AddToDatabase(string result)
        {
            using (var ctx = new ResultContext())
            {
                var results = new Result() { CalculationResult = result };

                ctx.Results.Add(results);
                ctx.SaveChanges();
            }
        }
    }
}

using System.Data.Entity;


namespace Repository
{
    public class ResultContext: DbContext
    {
        public ResultContext(): base("ResultDB")
        {
            
        }

        public DbSet<Result> Results { get; set; }

    }
}

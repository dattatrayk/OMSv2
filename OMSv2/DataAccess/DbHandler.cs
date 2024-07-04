using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace OMSv2.Service
{
    public class DbHandler
    {
        public static SqlDatabase GetDatabase()
        {
            return new SqlDatabase(GetConntionString());
        }
        private static string GetConntionString()
        {
            return "Data Source=FLEETROOT2\\SQLEXPRESS; Initial Catalog=OMSV22;Integrated Security=True";
        }
    }
}

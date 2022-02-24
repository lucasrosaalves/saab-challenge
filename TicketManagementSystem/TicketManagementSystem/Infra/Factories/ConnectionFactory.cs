using System.Configuration;
using System.Data.SqlClient;

namespace TicketManagementSystem.Infra.Factories
{
    public static class ConnectionFactory
    {
        private readonly static  string _connectionString = ConfigurationManager.ConnectionStrings["database"]?.ConnectionString;

        public static SqlConnection CreateSqlConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

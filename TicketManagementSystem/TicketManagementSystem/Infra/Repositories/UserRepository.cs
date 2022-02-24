using System;
using System.Data;
using System.Data.SqlClient;
using TicketManagementSystem.Domain.Entities;
using TicketManagementSystem.Domain.Repositories;
using TicketManagementSystem.Infra.Factories;

namespace TicketManagementSystem.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User GetUser(string username)
        {
            // Assume this method does not need to change and is connected to a database with users populated.
            try
            {
                string sql = "SELECT TOP 1 FROM Users u WHERE u.Username == @p1";
                using var connection = ConnectionFactory.CreateSqlConnection();
                connection.Open();

                var command = new SqlCommand(sql, connection)
                {
                    CommandType = CommandType.Text,
                };

                command.Parameters.Add("@p1", SqlDbType.NVarChar).Value = username;

                return (User)command.ExecuteScalar();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetAccountManager()
        {
            // Assume this method does not need to change.
            return GetUser("Sarah");
        }
    }
}

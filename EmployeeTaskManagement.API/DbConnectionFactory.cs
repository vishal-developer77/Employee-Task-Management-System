using Microsoft.Data.SqlClient;

namespace EmployeeTaskManagement.API.Data
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection CreateConnection()
        {
            string? connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Database connection string is not configured.");
            }

            return new SqlConnection(connectionString);
        }
    }
}
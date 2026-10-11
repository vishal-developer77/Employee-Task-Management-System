using EmployeeTaskManagement.API.Data;
using EmployeeTaskManagement.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeTaskManagement.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public AuthRepository(
            DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<User?> LoginAsync(
            string email,
            string password)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Auth_Login",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@Email",
                email);

            await connection.OpenAsync();

            using var reader =
                await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new User
                {
                    UserId =
                        Convert.ToInt32(reader["UserId"]),

                    FullName =
                        reader["FullName"]?.ToString()
                        ?? string.Empty,

                    Email =
                        reader["Email"]?.ToString()
                        ?? string.Empty,

                    PasswordHash =
                        reader["PasswordHash"]?.ToString()
                        ?? string.Empty,

                    Role =
                        reader["Role"]?.ToString()
                        ?? string.Empty,

                    DepartmentId =
                        reader["DepartmentId"] == DBNull.Value
                            ? 0
                            : Convert.ToInt32(
                                reader["DepartmentId"]),

                    IsActive =
                        Convert.ToBoolean(
                            reader["IsActive"]),

                    CreatedAt =
                        Convert.ToDateTime(
                            reader["CreatedAt"])
                };
            }

            return null;
        }
    }
}
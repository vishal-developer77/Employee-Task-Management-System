using EmployeeTaskManagement.API.Data;
using EmployeeTaskManagement.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeTaskManagement.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public UserRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // GET ALL USERS
        public async Task<List<User>> GetAllAsync()
        {
            var users = new List<User>();

            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_User_GetAll",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                users.Add(new User
                {
                    UserId = Convert.ToInt32(reader["UserId"]),

                    FullName = reader["FullName"].ToString()
                        ?? string.Empty,

                    Email = reader["Email"].ToString()
                        ?? string.Empty,

                    Role = reader["Role"].ToString()
                        ?? string.Empty,

                    DepartmentId = reader["DepartmentId"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(reader["DepartmentId"]),

                    IsActive = Convert.ToBoolean(reader["IsActive"]),

                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                });
            }

            return users;
        }


        // GET USER BY ID
        public async Task<User?> GetByIdAsync(int userId)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_User_GetById",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@UserId",
                userId);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new User
                {
                    UserId = Convert.ToInt32(reader["UserId"]),

                    FullName = reader["FullName"].ToString()
                        ?? string.Empty,

                    Email = reader["Email"].ToString()
                        ?? string.Empty,

                    Role = reader["Role"].ToString()
                        ?? string.Empty,

                    DepartmentId = reader["DepartmentId"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(reader["DepartmentId"]),

                    IsActive = Convert.ToBoolean(reader["IsActive"]),

                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                };
            }

            return null;
        }


        // INSERT USER
        public async Task<int> InsertAsync(
            string fullName,
            string email,
            string? passwordHash,
            string role,
            int? departmentId)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_User_Insert",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@FullName",
                fullName);

            command.Parameters.AddWithValue(
                "@Email",
                email);

            command.Parameters.AddWithValue(
                "@PasswordHash",
                (object?)passwordHash ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@Role",
                role);

            command.Parameters.AddWithValue(
                "@DepartmentId",
                (object?)departmentId ?? DBNull.Value);

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }


        // UPDATE USER
        public async Task<bool> UpdateAsync(
            int userId,
            string fullName,
            string email,
            string role,
            int? departmentId,
            bool isActive)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_User_Update",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@UserId",
                userId);

            command.Parameters.AddWithValue(
                "@FullName",
                fullName);

            command.Parameters.AddWithValue(
                "@Email",
                email);

            command.Parameters.AddWithValue(
                "@Role",
                role);

            command.Parameters.AddWithValue(
                "@DepartmentId",
                (object?)departmentId ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@IsActive",
                isActive);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }


        // DELETE USER
        public async Task<bool> DeleteAsync(int userId)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_User_Delete",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@UserId",
                userId);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}
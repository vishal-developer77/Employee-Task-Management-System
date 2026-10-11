using EmployeeTaskManagement.API.Data;
using EmployeeTaskManagement.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeTaskManagement.API.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public DepartmentRepository(
            DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // Get All Departments
        public async Task<List<Department>> GetAllAsync()
        {
            var departments = new List<Department>();

            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Department_GetAll",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader =
                await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                departments.Add(new Department
                {
                    DepartmentId =
                        Convert.ToInt32(
                            reader["DepartmentId"]),

                    DepartmentName =
                        reader["DepartmentName"]
                            .ToString() ?? string.Empty,

                    Description =
                        reader["Description"] == DBNull.Value
                            ? null
                            : reader["Description"].ToString(),

                    IsActive =
                        Convert.ToBoolean(
                            reader["IsActive"]),

                    CreatedAt =
                        Convert.ToDateTime(
                            reader["CreatedAt"])
                });
            }

            return departments;
        }

        // Get Department By ID
        public async Task<Department?> GetByIdAsync(
            int departmentId)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Department_GetById",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@DepartmentId",
                departmentId);

            await connection.OpenAsync();

            using var reader =
                await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Department
                {
                    DepartmentId =
                        Convert.ToInt32(
                            reader["DepartmentId"]),

                    DepartmentName =
                        reader["DepartmentName"]
                            .ToString() ?? string.Empty,

                    Description =
                        reader["Description"] == DBNull.Value
                            ? null
                            : reader["Description"].ToString(),

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

        // Insert Department
        public async Task<int> InsertAsync(
            string departmentName,
            string? description)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Department_Insert",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@DepartmentName",
                departmentName);

            command.Parameters.AddWithValue(
                "@Description",
                (object?)description ?? DBNull.Value);

            await connection.OpenAsync();

            var result =
                await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        // Update Department
        public async Task<bool> UpdateAsync(
            int departmentId,
            string departmentName,
            string? description,
            bool isActive)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Department_Update",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@DepartmentId",
                departmentId);

            command.Parameters.AddWithValue(
                "@DepartmentName",
                departmentName);

            command.Parameters.AddWithValue(
                "@Description",
                (object?)description ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@IsActive",
                isActive);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }

        // Delete Department
        public async Task<bool> DeleteAsync(
            int departmentId)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Department_Delete",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@DepartmentId",
                departmentId);

            await connection.OpenAsync();

            var result =
                await command.ExecuteNonQueryAsync();

            return result > 0;
        }
    }
}
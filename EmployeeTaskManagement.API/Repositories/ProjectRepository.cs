using EmployeeTaskManagement.API.Data;
using EmployeeTaskManagement.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeTaskManagement.API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public ProjectRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // GET ALL PROJECTS
        public async Task<List<Project>> GetAllAsync()
        {
            var projects = new List<Project>();

            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Project_GetAll",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                projects.Add(new Project
                {
                    ProjectId = Convert.ToInt32(
                        reader["ProjectId"]),

                    ProjectName = reader["ProjectName"]?.ToString()
                        ?? string.Empty,

                    Description = reader["Description"] == DBNull.Value
                        ? null
                        : reader["Description"]?.ToString(),

                    StartDate = Convert.ToDateTime(
                        reader["StartDate"]),

                    EndDate = reader["EndDate"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(
                            reader["EndDate"]),

                    Status = reader["Status"]?.ToString()
                        ?? string.Empty,

                    CreatedBy = Convert.ToInt32(
                        reader["CreatedBy"]),

                    CreatedByName = reader["CreatedByName"]?.ToString()
                        ?? string.Empty,

                    CreatedAt = Convert.ToDateTime(
                        reader["CreatedAt"])
                });
            }

            return projects;
        }

        // GET PROJECT BY ID
        public async Task<Project?> GetByIdAsync(int projectId)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Project_GetById",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@ProjectId",
                projectId);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Project
                {
                    ProjectId = Convert.ToInt32(
                        reader["ProjectId"]),

                    ProjectName = reader["ProjectName"]?.ToString()
                        ?? string.Empty,

                    Description = reader["Description"] == DBNull.Value
                        ? null
                        : reader["Description"]?.ToString(),

                    StartDate = Convert.ToDateTime(
                        reader["StartDate"]),

                    EndDate = reader["EndDate"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(
                            reader["EndDate"]),

                    Status = reader["Status"]?.ToString()
                        ?? string.Empty,

                    CreatedBy = Convert.ToInt32(
                        reader["CreatedBy"]),

                    CreatedByName = reader["CreatedByName"]?.ToString()
                        ?? string.Empty,

                    CreatedAt = Convert.ToDateTime(
                        reader["CreatedAt"])
                };
            }

            return null;
        }

        // INSERT PROJECT
        public async Task<int> InsertAsync(
            string projectName,
            string? description,
            DateTime startDate,
            DateTime? endDate,
            int createdBy,
            string status)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Project_Insert",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@ProjectName",
                projectName);

            command.Parameters.AddWithValue(
                "@Description",
                (object?)description ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@StartDate",
                startDate);

            command.Parameters.AddWithValue(
                "@EndDate",
                (object?)endDate ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@Status",
                status);

            command.Parameters.AddWithValue(
                "@CreatedBy",
                createdBy);

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        // UPDATE PROJECT
        public async Task<bool> UpdateAsync(
            int projectId,
            string projectName,
            string? description,
            DateTime startDate,
            DateTime? endDate,
            string status)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Project_Update",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@ProjectId",
                projectId);

            command.Parameters.AddWithValue(
                "@ProjectName",
                projectName);

            command.Parameters.AddWithValue(
                "@Description",
                (object?)description ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@StartDate",
                startDate);

            command.Parameters.AddWithValue(
                "@EndDate",
                (object?)endDate ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@Status",
                status);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }

        // DELETE PROJECT
        public async Task<bool> DeleteAsync(int projectId)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Project_Delete",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@ProjectId",
                projectId);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}
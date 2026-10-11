using EmployeeTaskManagement.API.Data;
using EmployeeTaskManagement.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeTaskManagement.API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public TaskRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // GET ALL TASKS
        public async Task<List<Models.Task>> GetAllAsync()
        {
            var tasks = new List<Models.Task>();

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("sp_Task_GetAll", connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                tasks.Add(new Models.Task
                {
                    TaskId = Convert.ToInt32(reader.GetValue(0)),

                    ProjectId = Convert.ToInt32(reader.GetValue(1)),

                    ProjectName = reader.GetValue(2) == DBNull.Value
                        ? null
                        : reader.GetValue(2).ToString(),

                    AssignedTo = Convert.ToInt32(reader.GetValue(3)),

                    AssignedEmployee = reader.GetValue(4) == DBNull.Value
                        ? null
                        : reader.GetValue(4).ToString(),

                    TaskTitle = reader.GetValue(5) == DBNull.Value
                        ? string.Empty
                        : reader.GetValue(5).ToString() ?? string.Empty,

                    Description = reader.GetValue(6) == DBNull.Value
                        ? null
                        : reader.GetValue(6).ToString(),

                    Priority = reader.GetValue(7) == DBNull.Value
                        ? string.Empty
                        : reader.GetValue(7).ToString() ?? string.Empty,

                    Status = reader.GetValue(8) == DBNull.Value
                        ? string.Empty
                        : reader.GetValue(8).ToString() ?? string.Empty,

                    DueDate = reader.GetValue(9) == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader.GetValue(9)),

                    CreatedBy = Convert.ToInt32(reader.GetValue(10)),

                    CreatedByName = reader.GetValue(11) == DBNull.Value
                        ? null
                        : reader.GetValue(11).ToString(),

                    CreatedAt = Convert.ToDateTime(reader.GetValue(12)),

                    UpdatedAt = reader.GetValue(13) == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader.GetValue(13))
                });
            }

            return tasks;
        }


        // GET TASK BY ID
        public async Task<Models.Task?> GetByIdAsync(int taskId)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Task_GetById",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@TaskId",
                taskId);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Models.Task
                {
                    TaskId = Convert.ToInt32(reader.GetValue(0)),

                    ProjectId = Convert.ToInt32(reader.GetValue(1)),

                    ProjectName = reader.GetValue(2) == DBNull.Value
                        ? null
                        : reader.GetValue(2).ToString(),

                    AssignedTo = Convert.ToInt32(reader.GetValue(3)),

                    AssignedEmployee = reader.GetValue(4) == DBNull.Value
                        ? null
                        : reader.GetValue(4).ToString(),

                    TaskTitle = reader.GetValue(5) == DBNull.Value
                        ? string.Empty
                        : reader.GetValue(5).ToString() ?? string.Empty,

                    Description = reader.GetValue(6) == DBNull.Value
                        ? null
                        : reader.GetValue(6).ToString(),

                    Priority = reader.GetValue(7) == DBNull.Value
                        ? string.Empty
                        : reader.GetValue(7).ToString() ?? string.Empty,

                    Status = reader.GetValue(8) == DBNull.Value
                        ? string.Empty
                        : reader.GetValue(8).ToString() ?? string.Empty,

                    DueDate = reader.GetValue(9) == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader.GetValue(9)),

                    CreatedBy = Convert.ToInt32(reader.GetValue(10)),

                    CreatedByName = reader.GetValue(11) == DBNull.Value
                        ? null
                        : reader.GetValue(11).ToString(),

                    CreatedAt = Convert.ToDateTime(reader.GetValue(12)),

                    UpdatedAt = reader.GetValue(13) == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader.GetValue(13))
                };
            }

            return null;
        }


        // INSERT TASK
        public async Task<int> InsertAsync(
            int projectId,
            int assignedTo,
            string taskName,
            string? description,
            string priority,
            string status,
            DateTime? dueDate,
            int createdBy)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Task_Insert",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@ProjectId",
                projectId);

            command.Parameters.AddWithValue(
                "@AssignedTo",
                assignedTo);

            command.Parameters.AddWithValue(
                "@TaskTitle",
                taskName);

            command.Parameters.AddWithValue(
                "@Description",
                (object?)description ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@Priority",
                priority);

            command.Parameters.AddWithValue(
                "@Status",
                status);

            command.Parameters.AddWithValue(
                "@DueDate",
                (object?)dueDate ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@CreatedBy",
                createdBy);

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }


        // UPDATE TASK
        public async Task<bool> UpdateAsync(
            int taskId,
            int projectId,
            int assignedTo,
            string taskName,
            string? description,
            string priority,
            string status,
            DateTime? dueDate,
            int createdBy)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Task_Update",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            // sp_Task_Update only accepts these 7 parameters

            command.Parameters.AddWithValue(
                "@TaskId",
                taskId);

            command.Parameters.AddWithValue(
                "@TaskTitle",
                taskName);

            command.Parameters.AddWithValue(
                "@Description",
                (object?)description ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@Priority",
                priority);

            command.Parameters.AddWithValue(
                "@Status",
                status);

            command.Parameters.AddWithValue(
                "@DueDate",
                (object?)dueDate ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@AssignedTo",
                assignedTo);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }


        // DELETE TASK
        public async Task<bool> DeleteAsync(int taskId)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Task_Delete",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@TaskId",
                taskId);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}
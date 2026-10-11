using EmployeeTaskManagement.API.Data;
using EmployeeTaskManagement.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeTaskManagement.API.Repositories
{
    public class TaskCommentRepository : ITaskCommentRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public TaskCommentRepository(
            DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // GET ALL COMMENTS
        public async Task<List<TaskComment>> GetAllAsync()
        {
            var comments = new List<TaskComment>();

            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_TaskComment_GetAll",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader =
                await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                comments.Add(new TaskComment
                {
                    CommentId =
                        Convert.ToInt32(reader.GetValue(0)),

                    TaskId =
                        Convert.ToInt32(reader.GetValue(1)),

                    TaskTitle =
                        reader.GetValue(2) == DBNull.Value
                            ? null
                            : reader.GetValue(2).ToString(),

                    UserId =
                        Convert.ToInt32(reader.GetValue(3)),

                    UserName =
                        reader.GetValue(4) == DBNull.Value
                            ? null
                            : reader.GetValue(4).ToString(),

                    CommentText =
                        reader.GetValue(5) == DBNull.Value
                            ? string.Empty
                            : reader.GetValue(5).ToString()
                              ?? string.Empty,

                    CreatedAt =
                        Convert.ToDateTime(reader.GetValue(6)),

                    // sp_TaskComment_GetAll
                    // does not return UpdatedAt
                    UpdatedAt = null
                });
            }

            return comments;
        }

        // GET COMMENT BY ID
        public async Task<TaskComment?> GetByIdAsync(
            int commentId)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_TaskComment_GetById",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@CommentId",
                commentId);

            await connection.OpenAsync();

            using var reader =
                await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new TaskComment
                {
                    CommentId =
                        Convert.ToInt32(reader.GetValue(0)),

                    TaskId =
                        Convert.ToInt32(reader.GetValue(1)),

                    TaskTitle =
                        reader.GetValue(2) == DBNull.Value
                            ? null
                            : reader.GetValue(2).ToString(),

                    UserId =
                        Convert.ToInt32(reader.GetValue(3)),

                    UserName =
                        reader.GetValue(4) == DBNull.Value
                            ? null
                            : reader.GetValue(4).ToString(),

                    CommentText =
                        reader.GetValue(5) == DBNull.Value
                            ? string.Empty
                            : reader.GetValue(5).ToString()
                              ?? string.Empty,

                    CreatedAt =
                        Convert.ToDateTime(reader.GetValue(6)),

                    UpdatedAt = null
                };
            }

            return null;
        }

        // INSERT COMMENT
        public async Task<int> InsertAsync(
            int taskId,
            int userId,
            string commentText)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_TaskComment_Insert",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@TaskId",
                taskId);

            command.Parameters.AddWithValue(
                "@UserId",
                userId);

            command.Parameters.AddWithValue(
                "@CommentText",
                commentText);

            await connection.OpenAsync();

            var result =
                await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        // UPDATE COMMENT
        public async Task<bool> UpdateAsync(
            int commentId,
            string commentText)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_TaskComment_Update",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@CommentId",
                commentId);

            command.Parameters.AddWithValue(
                "@CommentText",
                commentText);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }

        // DELETE COMMENT
        public async Task<bool> DeleteAsync(
            int commentId)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_TaskComment_Delete",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@CommentId",
                commentId);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}
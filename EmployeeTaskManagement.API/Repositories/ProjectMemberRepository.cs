using EmployeeTaskManagement.API.Data;
using EmployeeTaskManagement.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeTaskManagement.API.Repositories
{
    public class ProjectMemberRepository : IProjectMemberRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public ProjectMemberRepository(
            DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // GET ALL PROJECT MEMBERS
        public async Task<List<ProjectMember>> GetAllAsync()
        {
            var projectMembers = new List<ProjectMember>();

            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_ProjectMember_GetAll",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader =
                await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                projectMembers.Add(new ProjectMember
                {
                    ProjectMemberId =
                        Convert.ToInt32(
                            reader["ProjectMemberId"]),

                    ProjectId =
                        Convert.ToInt32(
                            reader["ProjectId"]),

                    ProjectName =
                        reader["ProjectName"] == DBNull.Value
                            ? null
                            : reader["ProjectName"].ToString(),

                    UserId =
                        Convert.ToInt32(
                            reader["UserId"]),

                    EmployeeName =
                        reader["EmployeeName"] == DBNull.Value
                            ? null
                            : reader["EmployeeName"].ToString(),

                    Email =
                        reader["Email"] == DBNull.Value
                            ? null
                            : reader["Email"].ToString(),

                    JoinedAt =
                        Convert.ToDateTime(
                            reader["JoinedAt"]),

                    IsActive =
                        Convert.ToBoolean(
                            reader["IsActive"])
                });
            }

            return projectMembers;
        }

        // GET PROJECT MEMBER BY ID
        public async Task<ProjectMember?> GetByIdAsync(
            int projectMemberId)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_ProjectMember_GetById",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@ProjectMemberId",
                projectMemberId);

            await connection.OpenAsync();

            using var reader =
                await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new ProjectMember
                {
                    ProjectMemberId =
                        Convert.ToInt32(
                            reader["ProjectMemberId"]),

                    ProjectId =
                        Convert.ToInt32(
                            reader["ProjectId"]),

                    ProjectName =
                        reader["ProjectName"] == DBNull.Value
                            ? null
                            : reader["ProjectName"].ToString(),

                    UserId =
                        Convert.ToInt32(
                            reader["UserId"]),

                    EmployeeName =
                        reader["EmployeeName"] == DBNull.Value
                            ? null
                            : reader["EmployeeName"].ToString(),

                    Email =
                        reader["Email"] == DBNull.Value
                            ? null
                            : reader["Email"].ToString(),

                    JoinedAt =
                        Convert.ToDateTime(
                            reader["JoinedAt"]),

                    IsActive =
                        Convert.ToBoolean(
                            reader["IsActive"])
                };
            }

            return null;
        }

        // INSERT PROJECT MEMBER
        public async Task<int> InsertAsync(
            int projectId,
            int userId)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_ProjectMember_Insert",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@ProjectId",
                projectId);

            command.Parameters.AddWithValue(
                "@UserId",
                userId);

            await connection.OpenAsync();

            var result =
                await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        // UPDATE PROJECT MEMBER
        public async Task<bool> UpdateAsync(
            int projectMemberId,
            bool isActive)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_ProjectMember_Update",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@ProjectMemberId",
                projectMemberId);

            command.Parameters.AddWithValue(
                "@IsActive",
                isActive);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }

        // DELETE PROJECT MEMBER
        public async Task<bool> DeleteAsync(
            int projectMemberId)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_ProjectMember_Delete",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@ProjectMemberId",
                projectMemberId);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}
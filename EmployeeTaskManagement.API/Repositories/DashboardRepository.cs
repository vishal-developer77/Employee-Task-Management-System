using EmployeeTaskManagement.API.Data;
using EmployeeTaskManagement.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeTaskManagement.API.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public DashboardRepository(
            DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Dashboard> GetAdminStatsAsync()
        {
            var dashboard = new Dashboard();

            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Dashboard_GetAdminStats",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader =
                await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                dashboard.TotalEmployees =
                    Convert.ToInt32(reader["TotalEmployees"]);

                dashboard.ActiveEmployees =
                    Convert.ToInt32(reader["ActiveEmployees"]);

                dashboard.TotalDepartments =
                    Convert.ToInt32(reader["TotalDepartments"]);

                dashboard.TotalProjects =
                    Convert.ToInt32(reader["TotalProjects"]);

                dashboard.TotalTasks =
                    Convert.ToInt32(reader["TotalTasks"]);

                dashboard.PendingTasks =
                    Convert.ToInt32(reader["PendingTasks"]);

                dashboard.CompletedTasks =
                    Convert.ToInt32(reader["CompletedTasks"]);

                dashboard.UnreadNotifications =
                    Convert.ToInt32(reader["UnreadNotifications"]);
            }

            return dashboard;
        }
    }
}
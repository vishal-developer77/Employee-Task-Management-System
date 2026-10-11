using EmployeeTaskManagement.API.Data;
using EmployeeTaskManagement.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeTaskManagement.API.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public NotificationRepository(
            DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // GET ALL NOTIFICATIONS
        public async Task<List<Notification>> GetAllAsync()
        {
            var notifications = new List<Notification>();

            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Notification_GetAll",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader =
                await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                notifications.Add(new Notification
                {
                    NotificationId =
                        Convert.ToInt32(reader.GetValue(0)),

                    UserId =
                        Convert.ToInt32(reader.GetValue(1)),

                    UserName =
                        reader.GetValue(2) == DBNull.Value
                            ? null
                            : reader.GetValue(2).ToString(),

                    Message =
                        reader.GetValue(3) == DBNull.Value
                            ? string.Empty
                            : reader.GetValue(3).ToString()
                              ?? string.Empty,

                    NotificationType =
                        reader.GetValue(4) == DBNull.Value
                            ? string.Empty
                            : reader.GetValue(4).ToString()
                              ?? string.Empty,

                    IsRead =
                        Convert.ToBoolean(reader.GetValue(5)),

                    CreatedAt =
                        Convert.ToDateTime(reader.GetValue(6))
                });
            }

            return notifications;
        }

        // GET NOTIFICATION BY ID
        public async Task<Notification?> GetByIdAsync(
            int notificationId)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Notification_GetById",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@NotificationId",
                notificationId);

            await connection.OpenAsync();

            using var reader =
                await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Notification
                {
                    NotificationId =
                        Convert.ToInt32(reader.GetValue(0)),

                    UserId =
                        Convert.ToInt32(reader.GetValue(1)),

                    UserName =
                        reader.GetValue(2) == DBNull.Value
                            ? null
                            : reader.GetValue(2).ToString(),

                    Message =
                        reader.GetValue(3) == DBNull.Value
                            ? string.Empty
                            : reader.GetValue(3).ToString()
                              ?? string.Empty,

                    NotificationType =
                        reader.GetValue(4) == DBNull.Value
                            ? string.Empty
                            : reader.GetValue(4).ToString()
                              ?? string.Empty,

                    IsRead =
                        Convert.ToBoolean(reader.GetValue(5)),

                    CreatedAt =
                        Convert.ToDateTime(reader.GetValue(6))
                };
            }

            return null;
        }

        // INSERT NOTIFICATION
        public async Task<int> InsertAsync(
            int userId,
            string message,
            string notificationType)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Notification_Insert",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@UserId",
                userId);

            command.Parameters.AddWithValue(
                "@Message",
                message);

            command.Parameters.AddWithValue(
                "@NotificationType",
                notificationType);

            await connection.OpenAsync();

            var result =
                await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        // MARK NOTIFICATION AS READ
        public async Task<bool> MarkAsReadAsync(
            int notificationId)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Notification_MarkAsRead",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@NotificationId",
                notificationId);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }

        // DELETE NOTIFICATION
        public async Task<bool> DeleteAsync(
            int notificationId)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Notification_Delete",
                connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@NotificationId",
                notificationId);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}
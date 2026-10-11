using EmployeeTaskManagement.API.Models;

namespace EmployeeTaskManagement.API.Repositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetAllAsync();

        Task<Notification?> GetByIdAsync(
            int notificationId);

        Task<int> InsertAsync(
            int userId,
            string message,
            string notificationType);

        Task<bool> MarkAsReadAsync(
            int notificationId);

        Task<bool> DeleteAsync(
            int notificationId);
    }
}
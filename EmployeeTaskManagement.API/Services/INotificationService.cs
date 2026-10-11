using EmployeeTaskManagement.API.DTOs;

namespace EmployeeTaskManagement.API.Services
{
    public interface INotificationService
    {
        Task<List<NotificationResponseDto>> GetAllAsync();

        Task<NotificationResponseDto?> GetByIdAsync(
            int notificationId);

        Task<int> CreateAsync(
            NotificationCreateDto dto);

        Task<bool> MarkAsReadAsync(
            int notificationId);

        Task<bool> DeleteAsync(
            int notificationId);
    }
}
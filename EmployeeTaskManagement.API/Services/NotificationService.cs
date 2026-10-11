using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Repositories;

namespace EmployeeTaskManagement.API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(
            INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        // GET ALL NOTIFICATIONS
        public async Task<List<NotificationResponseDto>> GetAllAsync()
        {
            var notifications =
                await _notificationRepository.GetAllAsync();

            return notifications.Select(notification =>
                new NotificationResponseDto
                {
                    NotificationId =
                        notification.NotificationId,

                    UserId =
                        notification.UserId,

                    UserName =
                        notification.UserName,

                    Message =
                        notification.Message,

                    NotificationType =
                        notification.NotificationType,

                    IsRead =
                        notification.IsRead,

                    CreatedAt =
                        notification.CreatedAt
                }).ToList();
        }

        // GET NOTIFICATION BY ID
        public async Task<NotificationResponseDto?> GetByIdAsync(
            int notificationId)
        {
            if (notificationId <= 0)
                throw new ArgumentException(
                    "Valid NotificationId is required.");

            var notification =
                await _notificationRepository
                    .GetByIdAsync(notificationId);

            if (notification == null)
                return null;

            return new NotificationResponseDto
            {
                NotificationId =
                    notification.NotificationId,

                UserId =
                    notification.UserId,

                UserName =
                    notification.UserName,

                Message =
                    notification.Message,

                NotificationType =
                    notification.NotificationType,

                IsRead =
                    notification.IsRead,

                CreatedAt =
                    notification.CreatedAt
            };
        }

        // CREATE NOTIFICATION
        public async Task<int> CreateAsync(
            NotificationCreateDto dto)
        {
            if (dto.UserId <= 0)
                throw new ArgumentException(
                    "Valid UserId is required.");

            if (string.IsNullOrWhiteSpace(dto.Message))
                throw new ArgumentException(
                    "Message is required.");

            if (string.IsNullOrWhiteSpace(dto.NotificationType))
                throw new ArgumentException(
                    "NotificationType is required.");

            return await _notificationRepository.InsertAsync(
                dto.UserId,
                dto.Message,
                dto.NotificationType);
        }

        // MARK AS READ
        public async Task<bool> MarkAsReadAsync(
            int notificationId)
        {
            if (notificationId <= 0)
                throw new ArgumentException(
                    "Valid NotificationId is required.");

            return await _notificationRepository
                .MarkAsReadAsync(notificationId);
        }

        // DELETE NOTIFICATION
        public async Task<bool> DeleteAsync(
            int notificationId)
        {
            if (notificationId <= 0)
                throw new ArgumentException(
                    "Valid NotificationId is required.");

            return await _notificationRepository
                .DeleteAsync(notificationId);
        }
    }
}
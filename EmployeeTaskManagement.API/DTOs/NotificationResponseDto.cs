namespace EmployeeTaskManagement.API.DTOs
{
    public class NotificationResponseDto
    {
        public int NotificationId { get; set; }

        public int UserId { get; set; }

        public string? UserName { get; set; }

        public string Message { get; set; } = string.Empty;

        public string NotificationType { get; set; } = string.Empty;

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
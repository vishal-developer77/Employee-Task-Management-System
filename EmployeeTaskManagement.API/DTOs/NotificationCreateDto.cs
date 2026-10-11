namespace EmployeeTaskManagement.API.DTOs
{
    public class NotificationCreateDto
    {
        public int UserId { get; set; }

        public string Message { get; set; } = string.Empty;

        public string NotificationType { get; set; } = string.Empty;
    }
}
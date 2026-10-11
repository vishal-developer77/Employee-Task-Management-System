using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskManagement.API.Models
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }

        [Required]
        public string Action { get; set; } = string.Empty;

        public string? EntityName { get; set; }

        public int? EntityId { get; set; }

        public string? UserEmail { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
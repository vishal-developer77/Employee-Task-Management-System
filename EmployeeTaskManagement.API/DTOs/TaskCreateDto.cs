namespace EmployeeTaskManagement.API.DTOs
{
    public class TaskCreateDto
    {
        public int ProjectId { get; set; }

        public int AssignedTo { get; set; }

        public string TaskTitle { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Priority { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        public int CreatedBy { get; set; }
    }
}
namespace EmployeeTaskManagement.API.DTOs
{
    public class TaskResponseDto
    {
        public int TaskId { get; set; }

        public int ProjectId { get; set; }

        public string? ProjectName { get; set; }

        public int AssignedTo { get; set; }

        public string? AssignedEmployee { get; set; }

        public string TaskTitle { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Priority { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        public int CreatedBy { get; set; }

        public string? CreatedByName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
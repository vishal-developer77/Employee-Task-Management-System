namespace EmployeeTaskManagement.API.DTOs
{
    public class ProjectCreateDto
    {
        public string ProjectName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int CreatedBy { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
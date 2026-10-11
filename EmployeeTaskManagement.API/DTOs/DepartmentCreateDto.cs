namespace EmployeeTaskManagement.API.DTOs
{
    public class DepartmentCreateDto
    {
        public string DepartmentName { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
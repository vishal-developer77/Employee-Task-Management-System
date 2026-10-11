namespace EmployeeTaskManagement.API.DTOs
{
    public class UserCreateDto
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PasswordHash { get; set; }

        public string Role { get; set; } = string.Empty;

        public int? DepartmentId { get; set; }
    }
}
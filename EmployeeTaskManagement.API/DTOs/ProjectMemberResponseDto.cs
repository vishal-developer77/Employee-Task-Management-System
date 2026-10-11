namespace EmployeeTaskManagement.API.DTOs
{
    public class ProjectMemberResponseDto
    {
        public int ProjectMemberId { get; set; }

        public int ProjectId { get; set; }

        public string? ProjectName { get; set; }

        public int UserId { get; set; }

        public string? EmployeeName { get; set; }

        public string? Email { get; set; }

        public DateTime JoinedAt { get; set; }

        public bool IsActive { get; set; }
    }
}
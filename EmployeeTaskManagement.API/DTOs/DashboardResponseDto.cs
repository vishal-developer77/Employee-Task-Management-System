namespace EmployeeTaskManagement.API.DTOs
{
    public class DashboardResponseDto
    {
        public int TotalEmployees { get; set; }

        public int ActiveEmployees { get; set; }

        public int TotalDepartments { get; set; }

        public int TotalProjects { get; set; }

        public int TotalTasks { get; set; }

        public int PendingTasks { get; set; }

        public int CompletedTasks { get; set; }

        public int UnreadNotifications { get; set; }
    }
}
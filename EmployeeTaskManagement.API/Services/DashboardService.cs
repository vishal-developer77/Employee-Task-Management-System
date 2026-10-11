using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Repositories;

namespace EmployeeTaskManagement.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(
            IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardResponseDto> GetAdminStatsAsync()
        {
            var dashboard =
                await _dashboardRepository.GetAdminStatsAsync();

            return new DashboardResponseDto
            {
                TotalEmployees = dashboard.TotalEmployees,

                ActiveEmployees = dashboard.ActiveEmployees,

                TotalDepartments = dashboard.TotalDepartments,

                TotalProjects = dashboard.TotalProjects,

                TotalTasks = dashboard.TotalTasks,

                PendingTasks = dashboard.PendingTasks,

                CompletedTasks = dashboard.CompletedTasks,

                UnreadNotifications = dashboard.UnreadNotifications
            };
        }
    }
}
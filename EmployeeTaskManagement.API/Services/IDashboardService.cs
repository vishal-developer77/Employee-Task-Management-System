using EmployeeTaskManagement.API.DTOs;

namespace EmployeeTaskManagement.API.Services
{
    public interface IDashboardService
    {
        Task<DashboardResponseDto> GetAdminStatsAsync();
    }
}
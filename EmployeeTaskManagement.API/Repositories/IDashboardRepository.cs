using EmployeeTaskManagement.API.Models;

namespace EmployeeTaskManagement.API.Repositories
{
    public interface IDashboardRepository
    {
        Task<Dashboard> GetAdminStatsAsync();
    }
}
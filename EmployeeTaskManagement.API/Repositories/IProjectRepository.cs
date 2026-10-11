using EmployeeTaskManagement.API.Models;

namespace EmployeeTaskManagement.API.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();

        Task<Project?> GetByIdAsync(int projectId);

        Task<int> InsertAsync(
            string projectName,
            string? description,
            DateTime startDate,
            DateTime? endDate,
            int createdBy,
            string status);

        Task<bool> UpdateAsync(
            int projectId,
            string projectName,
            string? description,
            DateTime startDate,
            DateTime? endDate,
            string status);

        Task<bool> DeleteAsync(int projectId);
    }
}
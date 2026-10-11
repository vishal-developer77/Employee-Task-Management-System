using EmployeeTaskManagement.API.Models;

namespace EmployeeTaskManagement.API.Repositories
{
    public interface ITaskRepository
    {
        Task<List<Models.Task>> GetAllAsync();

        Task<Models.Task?> GetByIdAsync(
            int taskId);

        Task<int> InsertAsync(
            int projectId,
            int assignedTo,
            string taskName,
            string? description,
            string priority,
            string status,
            DateTime? dueDate,
            int createdBy);

        Task<bool> UpdateAsync(
            int taskId,
            int projectId,
            int assignedTo,
            string taskName,
            string? description,
            string priority,
            string status,
            DateTime? dueDate,
            int createdBy);

        Task<bool> DeleteAsync(
            int taskId);
    }
}
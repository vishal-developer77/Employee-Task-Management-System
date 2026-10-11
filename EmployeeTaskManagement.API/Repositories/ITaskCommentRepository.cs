using EmployeeTaskManagement.API.Models;

namespace EmployeeTaskManagement.API.Repositories
{
    public interface ITaskCommentRepository
    {
        Task<List<TaskComment>> GetAllAsync();

        Task<TaskComment?> GetByIdAsync(int commentId);

        Task<int> InsertAsync(
            int taskId,
            int userId,
            string commentText);

        Task<bool> UpdateAsync(
            int commentId,
            string commentText);

        Task<bool> DeleteAsync(int commentId);
    }
}
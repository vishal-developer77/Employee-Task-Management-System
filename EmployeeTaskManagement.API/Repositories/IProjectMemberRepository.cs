using EmployeeTaskManagement.API.Models;

namespace EmployeeTaskManagement.API.Repositories
{
    public interface IProjectMemberRepository
    {
        Task<List<ProjectMember>> GetAllAsync();

        Task<ProjectMember?> GetByIdAsync(
            int projectMemberId);

        Task<int> InsertAsync(
            int projectId,
            int userId);

        Task<bool> UpdateAsync(
            int projectMemberId,
            bool isActive);

        Task<bool> DeleteAsync(
            int projectMemberId);
    }
}
using EmployeeTaskManagement.API.DTOs;

namespace EmployeeTaskManagement.API.Services
{
    public interface IProjectMemberService
    {
        Task<List<ProjectMemberResponseDto>> GetAllAsync();

        Task<ProjectMemberResponseDto?> GetByIdAsync(
            int projectMemberId);

        Task<int> CreateAsync(
            ProjectMemberCreateDto dto);

        Task<bool> UpdateAsync(
            int projectMemberId,
            ProjectMemberUpdateDto dto);

        Task<bool> DeleteAsync(
            int projectMemberId);
    }
}
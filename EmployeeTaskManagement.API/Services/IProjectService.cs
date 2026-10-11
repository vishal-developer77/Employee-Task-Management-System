using EmployeeTaskManagement.API.DTOs;

namespace EmployeeTaskManagement.API.Services
{
    public interface IProjectService
    {
        Task<List<ProjectResponseDto>> GetAllAsync();

        Task<ProjectResponseDto?> GetByIdAsync(int projectId);

        Task<int> CreateAsync(ProjectCreateDto dto);

        Task<bool> UpdateAsync(
            int projectId,
            ProjectUpdateDto dto);

        Task<bool> DeleteAsync(int projectId);
    }
}
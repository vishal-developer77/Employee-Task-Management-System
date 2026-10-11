using EmployeeTaskManagement.API.DTOs;

namespace EmployeeTaskManagement.API.Services
{
    public interface ITaskService
    {
        Task<List<TaskResponseDto>> GetAllAsync();

        Task<TaskResponseDto?> GetByIdAsync(
            int taskId);

        Task<int> CreateAsync(
            TaskCreateDto dto);

        Task<bool> UpdateAsync(
            int taskId,
            TaskUpdateDto dto);

        Task<bool> DeleteAsync(
            int taskId);
    }
}
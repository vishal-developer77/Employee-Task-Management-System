using EmployeeTaskManagement.API.DTOs;

namespace EmployeeTaskManagement.API.Services
{
    public interface ITaskCommentService
    {
        Task<List<TaskCommentResponseDto>> GetAllAsync();

        Task<TaskCommentResponseDto?> GetByIdAsync(int commentId);

        Task<int> CreateAsync(TaskCommentCreateDto dto);

        Task<bool> UpdateAsync(
            int commentId,
            TaskCommentUpdateDto dto);

        Task<bool> DeleteAsync(int commentId);
    }
}
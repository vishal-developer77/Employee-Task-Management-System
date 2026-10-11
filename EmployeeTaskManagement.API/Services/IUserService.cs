using EmployeeTaskManagement.API.DTOs;

namespace EmployeeTaskManagement.API.Services
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllAsync();

        Task<UserResponseDto?> GetByIdAsync(int userId);

        Task<int> CreateAsync(UserCreateDto dto);

        Task<bool> UpdateAsync(
            int userId,
            UserUpdateDto dto);

        Task<bool> DeleteAsync(int userId);
    }
}
using EmployeeTaskManagement.API.DTOs;

namespace EmployeeTaskManagement.API.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(
            LoginRequestDto dto);
    }
}
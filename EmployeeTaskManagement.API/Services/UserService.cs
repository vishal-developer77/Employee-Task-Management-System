using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Repositories;

namespace EmployeeTaskManagement.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                DepartmentId = u.DepartmentId,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            }).ToList();
        }

        public async Task<UserResponseDto?> GetByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                return null;

            return new UserResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                DepartmentId = user.DepartmentId,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<int> CreateAsync(UserCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
            {
                throw new ArgumentException(
                    "Full name is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new ArgumentException(
                    "Email is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Role))
            {
                throw new ArgumentException(
                    "Role is required.");
            }

            return await _userRepository.InsertAsync(
                dto.FullName,
                dto.Email,
                dto.PasswordHash,
                dto.Role,
                dto.DepartmentId);
        }

        public async Task<bool> UpdateAsync(
            int userId,
            UserUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
            {
                throw new ArgumentException(
                    "Full name is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new ArgumentException(
                    "Email is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Role))
            {
                throw new ArgumentException(
                    "Role is required.");
            }

            return await _userRepository.UpdateAsync(
                userId,
                dto.FullName,
                dto.Email,
                dto.Role,
                dto.DepartmentId,
                dto.IsActive);
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            return await _userRepository.DeleteAsync(userId);
        }
    }
}
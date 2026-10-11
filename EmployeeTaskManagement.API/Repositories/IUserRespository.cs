using EmployeeTaskManagement.API.Models;

namespace EmployeeTaskManagement.API.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();

        Task<User?> GetByIdAsync(int userId);

        Task<int> InsertAsync(
            string fullName,
            string email,
            string? passwordHash,
            string role,
            int? departmentId);

        Task<bool> UpdateAsync(
            int userId,
            string fullName,
            string email,
            string role,
            int? departmentId,
            bool isActive);

        Task<bool> DeleteAsync(int userId);
    }
}
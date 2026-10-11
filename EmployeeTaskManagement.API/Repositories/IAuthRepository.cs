using EmployeeTaskManagement.API.Models;

namespace EmployeeTaskManagement.API.Repositories
{
    public interface IAuthRepository
    {
        Task<User?> LoginAsync(string email, string password);
    }
}
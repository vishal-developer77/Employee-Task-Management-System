using EmployeeTaskManagement.API.Models;

namespace EmployeeTaskManagement.API.Repositories
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();

        Task<Department?> GetByIdAsync(int departmentId);

        Task<int> InsertAsync(string departmentName, string? description);

        Task<bool> UpdateAsync(
            int departmentId,
            string departmentName,
            string? description,
            bool isActive);

        Task<bool> DeleteAsync(int departmentId);
    }
}
using EmployeeTaskManagement.API.Models;

namespace EmployeeTaskManagement.API.Repositories
{
    public interface IEmployeeRepository
    {
        Task<(List<Employee> Data, int TotalRecords)> GetAllAsync(
            string? search,
            string? department,
            bool? isActive,
            string? sortBy,
            string? sortOrder,
            int pageNumber,
            int pageSize);

        Task<Employee?> GetByIdAsync(int id);

        Task<Employee> AddAsync(Employee employee);

        Task<bool> UpdateAsync(Employee employee);

        Task<bool> DeleteAsync(int id);
    }
}
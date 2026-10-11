using EmployeeTaskManagement.API.DTOs;

namespace EmployeeTaskManagement.API.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentResponseDto>> GetAllAsync();

        Task<DepartmentResponseDto?> GetByIdAsync(int departmentId);

        Task<int> CreateAsync(DepartmentCreateDto dto);

        Task<bool> UpdateAsync(
            int departmentId,
            DepartmentUpdateDto dto);

        Task<bool> DeleteAsync(int departmentId);
    }
}
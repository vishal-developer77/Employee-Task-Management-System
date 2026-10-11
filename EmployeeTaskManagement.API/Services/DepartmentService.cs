using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Repositories;

namespace EmployeeTaskManagement.API.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(
            IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<List<DepartmentResponseDto>> GetAllAsync()
        {
            var departments =
                await _departmentRepository.GetAllAsync();

            return departments.Select(d => new DepartmentResponseDto
            {
                DepartmentId = d.DepartmentId,
                DepartmentName = d.DepartmentName,
                Description = d.Description,
                IsActive = d.IsActive,
                CreatedAt = d.CreatedAt
            }).ToList();
        }

        public async Task<DepartmentResponseDto?> GetByIdAsync(
            int departmentId)
        {
            var department =
                await _departmentRepository.GetByIdAsync(departmentId);

            if (department == null)
                return null;

            return new DepartmentResponseDto
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                Description = department.Description,
                IsActive = department.IsActive,
                CreatedAt = department.CreatedAt
            };
        }

        public async Task<int> CreateAsync(
            DepartmentCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.DepartmentName))
            {
                throw new ArgumentException(
                    "Department name is required.");
            }

            return await _departmentRepository.InsertAsync(
                dto.DepartmentName,
                dto.Description);
        }

        public async Task<bool> UpdateAsync(
            int departmentId,
            DepartmentUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.DepartmentName))
            {
                throw new ArgumentException(
                    "Department name is required.");
            }

            return await _departmentRepository.UpdateAsync(
                departmentId,
                dto.DepartmentName,
                dto.Description,
                dto.IsActive);
        }

        public async Task<bool> DeleteAsync(int departmentId)
        {
            return await _departmentRepository.DeleteAsync(
                departmentId);
        }
    }
}
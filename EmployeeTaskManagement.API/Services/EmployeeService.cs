using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Models;
using EmployeeTaskManagement.API.Repositories;

namespace EmployeeTaskManagement.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Get All Employees
        // Search + Filter + Sorting + Pagination
        public async Task<EmployeePaginationResponseDto> GetAllAsync(
            string? search,
            string? department,
            bool? isActive,
            string? sortBy,
            string? sortOrder,
            int pageNumber,
            int pageSize)
        {
            // Default values
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            if (pageSize > 100)
            {
                pageSize = 100;
            }

            // Get data + total records
            var result =
                await _employeeRepository.GetAllAsync(
                    search,
                    department,
                    isActive,
                    sortBy,
                    sortOrder,
                    pageNumber,
                    pageSize);

            // Calculate total pages
            int totalPages =
                (int)Math.Ceiling(
                    result.TotalRecords /
                    (double)pageSize);

            return new EmployeePaginationResponseDto
            {
                TotalRecords = result.TotalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                Data = result.Data
            };
        }

        // Get Employee By ID
        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        // Add Employee
        public async Task<Employee> AddAsync(
            Employee employee)
        {
            return await _employeeRepository.AddAsync(
                employee);
        }

        // Update Employee
        public async Task<bool> UpdateAsync(
            Employee employee)
        {
            return await _employeeRepository.UpdateAsync(
                employee);
        }

        // Delete Employee
        public async Task<bool> DeleteAsync(int id)
        {
            return await _employeeRepository.DeleteAsync(
                id);
        }
    }
}
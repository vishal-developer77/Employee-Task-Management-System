using EmployeeTaskManagement.API.Data;
using EmployeeTaskManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTaskManagement.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All Employees
        // Search + Filter + Sorting + Pagination
        public async Task<(List<Employee> Data, int TotalRecords)> GetAllAsync(
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

            // Maximum page size
            if (pageSize > 100)
            {
                pageSize = 100;
            }

            var query = _context.Employees
                .AsQueryable();

            // Search by Employee Name or Email
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e =>
                    e.EmployeeName.Contains(search) ||
                    e.Email.Contains(search));
            }

            // Filter by Department
            if (!string.IsNullOrWhiteSpace(department))
            {
                query = query.Where(e =>
                    e.Department == department);
            }

            // Filter by Active / Inactive
            if (isActive.HasValue)
            {
                query = query.Where(e =>
                    e.IsActive == isActive.Value);
            }

            // Sorting
            if (string.Equals(
                sortBy,
                "name",
                StringComparison.OrdinalIgnoreCase))
            {
                query = string.Equals(
                    sortOrder,
                    "desc",
                    StringComparison.OrdinalIgnoreCase)
                    ? query.OrderByDescending(e => e.EmployeeName)
                    : query.OrderBy(e => e.EmployeeName);
            }
            else if (string.Equals(
                sortBy,
                "salary",
                StringComparison.OrdinalIgnoreCase))
            {
                query = string.Equals(
                    sortOrder,
                    "desc",
                    StringComparison.OrdinalIgnoreCase)
                    ? query.OrderByDescending(e => e.Salary)
                    : query.OrderBy(e => e.Salary);
            }
            else if (string.Equals(
                sortBy,
                "department",
                StringComparison.OrdinalIgnoreCase))
            {
                query = string.Equals(
                    sortOrder,
                    "desc",
                    StringComparison.OrdinalIgnoreCase)
                    ? query.OrderByDescending(e => e.Department)
                    : query.OrderBy(e => e.Department);
            }
            else
            {
                // Default sorting
                query = query.OrderBy(e => e.EmployeeId);
            }

            // Total records BEFORE pagination
            int totalRecords =
                await query.CountAsync();

            // Pagination
            var employees =
                await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            return (employees, totalRecords);
        }

        // Get Employee By ID
        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e =>
                    e.EmployeeId == id);
        }

        // Add Employee
        public async Task<Employee> AddAsync(
            Employee employee)
        {
            var existingEmployee =
                await _context.Employees
                    .FirstOrDefaultAsync(e =>
                        e.Email == employee.Email);

            if (existingEmployee != null)
            {
                throw new InvalidOperationException(
                    "Employee with this email already exists.");
            }

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return employee;
        }

        // Update Employee
        public async Task<bool> UpdateAsync(
            Employee employee)
        {
            var existingEmployee =
                await _context.Employees
                    .FirstOrDefaultAsync(e =>
                        e.EmployeeId == employee.EmployeeId);

            if (existingEmployee == null)
            {
                return false;
            }

            // Check duplicate email
            var duplicateEmail =
                await _context.Employees
                    .AnyAsync(e =>
                        e.Email == employee.Email &&
                        e.EmployeeId != employee.EmployeeId);

            if (duplicateEmail)
            {
                throw new InvalidOperationException(
                    "Another employee with this email already exists.");
            }

            existingEmployee.EmployeeName =
                employee.EmployeeName;

            existingEmployee.Email =
                employee.Email;

            existingEmployee.Department =
                employee.Department;

            existingEmployee.Designation =
                employee.Designation;

            existingEmployee.Salary =
                employee.Salary;

            existingEmployee.IsActive =
                employee.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }

        // Delete Employee
        public async Task<bool> DeleteAsync(int id)
        {
            var employee =
                await _context.Employees
                    .FirstOrDefaultAsync(e =>
                        e.EmployeeId == id);

            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
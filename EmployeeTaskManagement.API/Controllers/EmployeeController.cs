using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Models;
using EmployeeTaskManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAuditLogService _auditLogService;

        public EmployeeController(
            IEmployeeService employeeService,
            IAuditLogService auditLogService)
        {
            _employeeService = employeeService;
            _auditLogService = auditLogService;
        }

        // GET: api/Employee
        // Search + Filter + Sorting + Pagination
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(
            string? search = null,
            string? department = null,
            bool? isActive = null,
            string? sortBy = null,
            string? sortOrder = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var employees =
                await _employeeService.GetAllAsync(
                    search,
                    department,
                    isActive,
                    sortBy,
                    sortOrder,
                    pageNumber,
                    pageSize);

            return Ok(
                new ApiResponseDto<object>
                {
                    Success = true,
                    Message = "Employees retrieved successfully.",
                    Data = employees
                });
        }

        // GET: api/Employee/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee =
                await _employeeService.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound(
                    new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = "Employee not found.",
                        Data = null
                    });
            }

            return Ok(
                new ApiResponseDto<Employee>
                {
                    Success = true,
                    Message = "Employee retrieved successfully.",
                    Data = employee
                });
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee(
            EmployeeCreateDto dto)
        {
            try
            {
                var employee = new Employee
                {
                    EmployeeName = dto.EmployeeName,
                    Email = dto.Email,
                    Department = dto.Department,
                    Designation = dto.Designation,
                    Salary = dto.Salary,
                    IsActive = dto.IsActive
                };

                var createdEmployee =
                    await _employeeService.AddAsync(employee);

                await _auditLogService.CreateAsync(
                    "CREATE",
                    "Employee",
                    createdEmployee.EmployeeId,
                    User.Identity?.Name,
                    $"Employee '{createdEmployee.EmployeeName}' was created.");

                return Ok(
                    new ApiResponseDto<Employee>
                    {
                        Success = true,
                        Message = "Employee created successfully.",
                        Data = createdEmployee
                    });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(
                    new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null
                    });
            }
        }

        // PUT: api/Employee/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(
            int id,
            EmployeeUpdateDto dto)
        {
            if (id != dto.EmployeeId)
            {
                return BadRequest(
                    new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = "Employee ID does not match.",
                        Data = null
                    });
            }

            try
            {
                var employee = new Employee
                {
                    EmployeeId = dto.EmployeeId,
                    EmployeeName = dto.EmployeeName,
                    Email = dto.Email,
                    Department = dto.Department,
                    Designation = dto.Designation,
                    Salary = dto.Salary,
                    IsActive = dto.IsActive
                };

                var updated =
                    await _employeeService.UpdateAsync(employee);

                if (!updated)
                {
                    return NotFound(
                        new ApiResponseDto<object>
                        {
                            Success = false,
                            Message = "Employee not found.",
                            Data = null
                        });
                }

                await _auditLogService.CreateAsync(
                    "UPDATE",
                    "Employee",
                    employee.EmployeeId,
                    User.Identity?.Name,
                    $"Employee '{employee.EmployeeName}' was updated.");

                return Ok(
                    new ApiResponseDto<Employee>
                    {
                        Success = true,
                        Message = "Employee updated successfully.",
                        Data = employee
                    });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(
                    new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null
                    });
            }
        }

        // DELETE: api/Employee/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee =
                await _employeeService.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound(
                    new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = "Employee not found.",
                        Data = null
                    });
            }

            var deleted =
                await _employeeService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(
                    new ApiResponseDto<object>
                    {
                        Success = false,
                        Message = "Employee not found.",
                        Data = null
                    });
            }

            await _auditLogService.CreateAsync(
                "DELETE",
                "Employee",
                id,
                User.Identity?.Name,
                $"Employee '{employee.EmployeeName}' was deleted.");

            return Ok(
                new ApiResponseDto<object>
                {
                    Success = true,
                    Message = "Employee deleted successfully.",
                    Data = null
                });
        }
    }
}
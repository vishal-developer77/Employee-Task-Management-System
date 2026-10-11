using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(
            IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments =
                await _departmentService.GetAllAsync();

            return Ok(departments);
        }

        // GET: api/Department/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department =
                await _departmentService.GetByIdAsync(id);

            if (department == null)
            {
                return NotFound("Department not found.");
            }

            return Ok(department);
        }

        // POST: api/Department
        [HttpPost]
        public async Task<IActionResult> Create(
            DepartmentCreateDto dto)
        {
            try
            {
                var departmentId =
                    await _departmentService.CreateAsync(dto);

                return Ok(new
                {
                    Message = "Department created successfully.",
                    DepartmentId = departmentId
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Department/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            DepartmentUpdateDto dto)
        {
            try
            {
                var result =
                    await _departmentService.UpdateAsync(id, dto);

                if (!result)
                {
                    return NotFound("Department not found.");
                }

                return Ok("Department updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Department/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _departmentService.DeleteAsync(id);

            if (!result)
            {
                return NotFound("Department not found.");
            }

            return Ok("Department deleted successfully.");
        }
    }
}
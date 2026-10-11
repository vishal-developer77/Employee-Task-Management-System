using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeTaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects =
                await _projectService.GetAllAsync();

            return Ok(projects);
        }


        // GET: api/Project/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project =
                await _projectService.GetByIdAsync(id);

            if (project == null)
                return NotFound("Project not found.");

            return Ok(project);
        }


        // POST: api/Project
        [HttpPost]
        public async Task<IActionResult> Create(
            ProjectCreateDto dto)
        {
            try
            {
                var projectId =
                    await _projectService.CreateAsync(dto);

                return Ok(new
                {
                    Message = "Project created successfully.",
                    ProjectId = projectId
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT: api/Project/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            ProjectUpdateDto dto)
        {
            try
            {
                var result =
                    await _projectService.UpdateAsync(id, dto);

                if (!result)
                    return NotFound("Project not found.");

                return Ok("Project updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE: api/Project/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result =
                    await _projectService.DeleteAsync(id);

                if (!result)
                    return NotFound("Project not found.");

                return Ok("Project deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
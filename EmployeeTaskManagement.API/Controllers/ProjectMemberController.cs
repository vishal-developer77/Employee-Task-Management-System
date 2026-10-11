using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectMemberController : ControllerBase
    {
        private readonly IProjectMemberService
            _projectMemberService;

        public ProjectMemberController(
            IProjectMemberService projectMemberService)
        {
            _projectMemberService =
                projectMemberService;
        }

        // GET: api/ProjectMember
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projectMembers =
                await _projectMemberService.GetAllAsync();

            return Ok(projectMembers);
        }

        // GET: api/ProjectMember/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id)
        {
            var projectMember =
                await _projectMemberService
                    .GetByIdAsync(id);

            if (projectMember == null)
            {
                return NotFound(
                    "Project member not found.");
            }

            return Ok(projectMember);
        }

        // POST: api/ProjectMember
        [HttpPost]
        public async Task<IActionResult> Create(
            ProjectMemberCreateDto dto)
        {
            try
            {
                var projectMemberId =
                    await _projectMemberService
                        .CreateAsync(dto);

                return Ok(new
                {
                    Message =
                        "Project member created successfully.",

                    ProjectMemberId =
                        projectMemberId
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/ProjectMember/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            ProjectMemberUpdateDto dto)
        {
            try
            {
                var result =
                    await _projectMemberService
                        .UpdateAsync(id, dto);

                if (!result)
                {
                    return NotFound(
                        "Project member not found.");
                }

                return Ok(
                    "Project member updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ProjectMember/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            try
            {
                var result =
                    await _projectMemberService
                        .DeleteAsync(id);

                if (!result)
                {
                    return NotFound(
                        "Project member not found.");
                }

                return Ok(
                    "Project member deleted successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
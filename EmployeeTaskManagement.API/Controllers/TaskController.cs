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
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(
            ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/Task
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks =
                await _taskService.GetAllAsync();

            return Ok(tasks);
        }

        // GET: api/Task/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id)
        {
            var task =
                await _taskService.GetByIdAsync(id);

            if (task == null)
            {
                return NotFound(
                    "Task not found.");
            }

            return Ok(task);
        }

        // POST: api/Task
        [HttpPost]
        public async Task<IActionResult> Create(
            TaskCreateDto dto)
        {
            try
            {
                var taskId =
                    await _taskService.CreateAsync(dto);

                return Ok(new
                {
                    Message =
                        "Task created successfully.",

                    TaskId = taskId
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

        // PUT: api/Task/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            TaskUpdateDto dto)
        {
            try
            {
                var result =
                    await _taskService.UpdateAsync(
                        id,
                        dto);

                if (!result)
                {
                    return NotFound(
                        "Task not found.");
                }

                return Ok(
                    "Task updated successfully.");
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

        // DELETE: api/Task/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            try
            {
                var result =
                    await _taskService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound(
                        "Task not found.");
                }

                return Ok(
                    "Task deleted successfully.");
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
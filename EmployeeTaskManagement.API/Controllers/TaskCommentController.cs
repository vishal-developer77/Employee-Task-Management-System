using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskCommentController : ControllerBase
    {
        private readonly ITaskCommentService _taskCommentService;

        public TaskCommentController(
            ITaskCommentService taskCommentService)
        {
            _taskCommentService = taskCommentService;
        }

        // GET: api/TaskComment
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments =
                await _taskCommentService.GetAllAsync();

            return Ok(comments);
        }

        // GET: api/TaskComment/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var comment =
                    await _taskCommentService.GetByIdAsync(id);

                if (comment == null)
                    return NotFound("Comment not found.");

                return Ok(comment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/TaskComment
        [HttpPost]
        public async Task<IActionResult> Create(
            TaskCommentCreateDto dto)
        {
            try
            {
                var commentId =
                    await _taskCommentService.CreateAsync(dto);

                return Ok(new
                {
                    Message = "Comment created successfully.",
                    CommentId = commentId
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

        // PUT: api/TaskComment/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            TaskCommentUpdateDto dto)
        {
            try
            {
                var result =
                    await _taskCommentService.UpdateAsync(id, dto);

                if (!result)
                    return NotFound("Comment not found.");

                return Ok("Comment updated successfully.");
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

        // DELETE: api/TaskComment/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result =
                    await _taskCommentService.DeleteAsync(id);

                if (!result)
                    return NotFound("Comment not found.");

                return Ok("Comment deleted successfully.");
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
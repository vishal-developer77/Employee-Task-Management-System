using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(
            INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // GET: api/Notification
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications =
                await _notificationService.GetAllAsync();

            return Ok(notifications);
        }

        // GET: api/Notification/1002
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var notification =
                    await _notificationService.GetByIdAsync(id);

                if (notification == null)
                    return NotFound("Notification not found.");

                return Ok(notification);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Notification
        [HttpPost]
        public async Task<IActionResult> Create(
            NotificationCreateDto dto)
        {
            try
            {
                var notificationId =
                    await _notificationService.CreateAsync(dto);

                return Ok(new
                {
                    Message = "Notification created successfully.",
                    NotificationId = notificationId
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

        // PUT: api/Notification/1002/read
        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            try
            {
                var result =
                    await _notificationService.MarkAsReadAsync(id);

                if (!result)
                    return NotFound("Notification not found.");

                return Ok("Notification marked as read.");
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

        // DELETE: api/Notification/1002
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result =
                    await _notificationService.DeleteAsync(id);

                if (!result)
                    return NotFound("Notification not found.");

                return Ok("Notification deleted successfully.");
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
using EmployeeTaskManagement.API.Models;
using EmployeeTaskManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogController(
            IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        // GET: api/AuditLog
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var logs =
                await _auditLogService.GetAllAsync();

            return Ok(logs);
        }

        // GET: api/AuditLog/user?email=admin@company.com
        [HttpGet("user")]
        public async Task<IActionResult> GetByUser(
            string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(
                    "Email is required.");
            }

            var logs =
                await _auditLogService.GetByUserAsync(
                    email);

            return Ok(logs);
        }
    }
}
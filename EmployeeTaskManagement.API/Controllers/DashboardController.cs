using EmployeeTaskManagement.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeTaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(
            IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // GET: api/Dashboard/admin
        [HttpGet("admin")]
        public async Task<IActionResult> GetAdminDashboard()
        {
            try
            {
                var dashboard =
                    await _dashboardService.GetAdminStatsAsync();

                return Ok(dashboard);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
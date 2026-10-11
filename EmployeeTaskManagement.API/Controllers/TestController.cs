using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok("Public API is working.");
        }

        [Authorize]
        [HttpGet("secure")]
        public IActionResult Secure()
        {
            return Ok("JWT Authentication is working.");
        }
    }
}
using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequestDto dto)
        {
            try
            {
                var result =
                    await _authService.LoginAsync(dto);

                if (result == null)
                {
                    return Unauthorized(
                        "Invalid email or password.");
                }

                return Ok(result);
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
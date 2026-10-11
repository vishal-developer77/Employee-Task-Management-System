using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            UserCreateDto dto)
        {
            try
            {
                var userId =
                    await _userService.CreateAsync(dto);

                return Ok(new
                {
                    Message = "User created successfully.",
                    UserId = userId
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UserUpdateDto dto)
        {
            try
            {
                var result =
                    await _userService.UpdateAsync(id, dto);

                if (!result)
                    return NotFound("User not found.");

                return Ok("User updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _userService.DeleteAsync(id);

            if (!result)
                return NotFound("User not found.");

            return Ok("User deleted successfully.");
        }
    }
}
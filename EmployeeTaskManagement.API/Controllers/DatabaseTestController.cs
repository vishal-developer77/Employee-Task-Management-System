using EmployeeTaskManagement.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseTestController : ControllerBase
    {
        private readonly DbConnectionFactory _connectionFactory;

        public DatabaseTestController(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestDatabaseConnection()
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();

                await connection.OpenAsync();

                return Ok(new
                {
                    Message = "Database connection successful",
                    Database = connection.Database,
                    Server = connection.DataSource
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Database connection failed",
                    Error = ex.Message
                });
            }
        }
    }
}
using System.Text.Json;

namespace EmployeeTaskManagement.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An unexpected error occurred.");

                await HandleExceptionAsync(
                    context,
                    ex);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred.",
                Details = exception.Message
            };

            var jsonResponse =
                JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
using EmployeeTaskManagement.API.Data;
using EmployeeTaskManagement.API.Middleware;
using EmployeeTaskManagement.API.Repositories;
using EmployeeTaskManagement.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

namespace EmployeeTaskManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ================= CONTROLLERS =================

            builder.Services.AddControllers();
            builder.Services.AddScoped<INotificationService, NotificationService>();


            // ================= DATABASE =================

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString(
                        "DefaultConnection")));

            builder.Services.AddScoped<DbConnectionFactory>();

            // ================= REPOSITORIES =================

            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

            // ================= SERVICES =================

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IAuditLogService, AuditLogService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();

            // ================= JWT AUTHENTICATION =================

            builder.Services
                .AddAuthentication(
                    JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(
                                        builder.Configuration[
                                            "Jwt:Key"]!))
                        };

                    // ================= JWT ERROR LOGGING =================

                    options.Events =
                        new JwtBearerEvents
                        {
                            OnAuthenticationFailed =
                                context =>
                                {
                                    var logger =
                                        context.HttpContext
                                            .RequestServices
                                            .GetRequiredService<
                                                ILogger<Program>>();

                                    logger.LogError(
                                        "JWT authentication failed: {Error}",
                                        context.Exception.Message);

                                    return Task.CompletedTask;
                                }
                        };
                });

            // ================= AUTHORIZATION =================

            builder.Services.AddAuthorization();

            // ================= SWAGGER =================

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Description =
                            "Enter your JWT token."
                    });

                options.AddSecurityRequirement(
                    document =>
                        new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecuritySchemeReference(
                                    "Bearer",
                                    document),
                                new List<string>()
                            }
                        });
            });

            // ================= CORS =================

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    "ReactPolicy",
                    policy =>
                    {
                        policy
                            .WithOrigins(
                                "http://localhost:5173",
                                "http://localhost:5174"
                            )
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // ================= BUILD APP =================

            var app = builder.Build();

            // ================= GLOBAL EXCEPTION =================

            app.UseMiddleware<GlobalExceptionMiddleware>();

            // ================= SWAGGER =================

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ================= HTTPS =================

            app.UseHttpsRedirection();

            // ================= CORS =================

            app.UseCors("ReactPolicy");

            // ================= AUTHENTICATION =================

            app.UseAuthentication();

            // ================= AUTHORIZATION =================

            app.UseAuthorization();

            // ================= CONTROLLERS =================

            app.MapControllers();

            // ================= RUN =================

            app.Run();
        }
    }
}
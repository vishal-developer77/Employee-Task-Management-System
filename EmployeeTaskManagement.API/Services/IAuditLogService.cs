using EmployeeTaskManagement.API.Models;

namespace EmployeeTaskManagement.API.Services
{
    public interface IAuditLogService
    {
        Task<AuditLog> CreateAsync(
            string action,
            string? entityName,
            int? entityId,
            string? userEmail,
            string? description);

        Task<List<AuditLog>> GetAllAsync();

        Task<List<AuditLog>> GetByUserAsync(
            string userEmail);
    }
}
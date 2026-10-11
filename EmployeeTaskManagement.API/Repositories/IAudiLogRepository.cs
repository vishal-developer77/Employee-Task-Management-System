using EmployeeTaskManagement.API.Models;

namespace EmployeeTaskManagement.API.Repositories
{
    public interface IAuditLogRepository
    {
        Task<AuditLog> AddAsync(AuditLog auditLog);

        Task<List<AuditLog>> GetAllAsync();

        Task<List<AuditLog>> GetByUserAsync(string userEmail);
    }
}
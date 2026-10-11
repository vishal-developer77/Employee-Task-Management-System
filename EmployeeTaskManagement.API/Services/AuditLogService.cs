using EmployeeTaskManagement.API.Models;
using EmployeeTaskManagement.API.Repositories;

namespace EmployeeTaskManagement.API.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogService(
            IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        // Create Audit Log
        public async Task<AuditLog> CreateAsync(
            string action,
            string? entityName,
            int? entityId,
            string? userEmail,
            string? description)
        {
            var auditLog = new AuditLog
            {
                Action = action,
                EntityName = entityName,
                EntityId = entityId,
                UserEmail = userEmail,
                Description = description,
                CreatedAt = DateTime.UtcNow
            };

            return await _auditLogRepository.AddAsync(
                auditLog);
        }

        // Get All Audit Logs
        public async Task<List<AuditLog>> GetAllAsync()
        {
            return await _auditLogRepository.GetAllAsync();
        }

        // Get Logs By User
        public async Task<List<AuditLog>> GetByUserAsync(
            string userEmail)
        {
            return await _auditLogRepository.GetByUserAsync(
                userEmail);
        }
    }
}
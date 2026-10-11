using EmployeeTaskManagement.API.Data;
using EmployeeTaskManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTaskManagement.API.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ApplicationDbContext _context;

        public AuditLogRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // Add Audit Log
        public async Task<AuditLog> AddAsync(
            AuditLog auditLog)
        {
            _context.AuditLogs.Add(auditLog);

            await _context.SaveChangesAsync();

            return auditLog;
        }

        // Get All Audit Logs
        public async Task<List<AuditLog>> GetAllAsync()
        {
            return await _context.AuditLogs
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        // Get Audit Logs By User
        public async Task<List<AuditLog>> GetByUserAsync(
            string userEmail)
        {
            return await _context.AuditLogs
                .Where(a => a.UserEmail == userEmail)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }
    }
}
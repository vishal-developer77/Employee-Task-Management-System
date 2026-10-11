using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskManagement.API.DTOs
{
    public class EmployeeCreateDto
    {
        [Required]
        [StringLength(100)]
        public string EmployeeName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;

        [Required]
        public string Designation { get; set; } = string.Empty;

        [Range(1, double.MaxValue)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
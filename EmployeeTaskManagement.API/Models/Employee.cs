using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskManagement.API.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee name is required.")]
        [StringLength(100, ErrorMessage = "Employee name cannot exceed 100 characters.")]
        public string EmployeeName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; } = string.Empty;

        [Required(ErrorMessage = "Designation is required.")]
        public string Designation { get; set; } = string.Empty;

        [Range(1, double.MaxValue, ErrorMessage = "Salary must be greater than 0.")]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
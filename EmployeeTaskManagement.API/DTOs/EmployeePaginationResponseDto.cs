using EmployeeTaskManagement.API.Models;

namespace EmployeeTaskManagement.API.DTOs
{
    public class EmployeePaginationResponseDto
    {
        public int TotalRecords { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public List<Employee> Data { get; set; } =
            new List<Employee>();
    }
}
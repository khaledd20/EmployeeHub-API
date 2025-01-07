namespace EmployeeHub.API.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; } // Nullable string
        public string? Email { get; set; }      // Nullable string
        public string? Address { get; set; }   // Nullable string
        public string? Photo { get; set; }     // Nullable string
        public int DepartmentID { get; set; }
        public Department? Department { get; set; } // Nullable navigation property
        public int RoleID { get; set; }
        public Role? Role { get; set; }         // Nullable navigation property
    }
}

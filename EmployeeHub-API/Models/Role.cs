namespace EmployeeHub.API.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}

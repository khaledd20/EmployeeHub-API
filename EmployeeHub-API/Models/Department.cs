namespace EmployeeHub.API.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int? ManagerID { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}

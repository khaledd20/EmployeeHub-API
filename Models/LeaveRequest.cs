namespace EmployeeHub.API.Models
{
    public class LeaveRequest
    {
        public int LeaveRequestID { get; set; }
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } // Pending, Approved, Denied
        public int? ManagerID { get; set; }
        public Employee Manager { get; set; }
    }
}

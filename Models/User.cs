namespace EmployeeHub.API.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }  // Changed from PasswordHash to Password
        public string Role { get; set; }
        public int? EmployeeID { get; set; }
    }
}

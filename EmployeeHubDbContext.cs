using Microsoft.EntityFrameworkCore;
using EmployeeHub.API.Models;

namespace EmployeeHub.API.Data
{
    public class EmployeeHubDbContext : DbContext
    {
        public EmployeeHubDbContext(DbContextOptions<EmployeeHubDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map entities to exact table names in the database
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<LeaveRequest>().ToTable("LeaveRequest");
        }
    }
}

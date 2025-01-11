using EmployeeHub.API.Data;
using EmployeeHub.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeHubDbContext _context;

        public EmployeesController(EmployeeHubDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.Include(e => e.Department).Include(e => e.Role).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployees), new { id = employee.EmployeeID }, employee);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.Include(e => e.Department).Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.EmployeeID == id);

            if (employee == null) return NotFound();

            return employee;
        }

        [HttpGet("departments")]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee updatedEmployee)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            employee.FirstName = updatedEmployee.FirstName;
            employee.LastName = updatedEmployee.LastName;
            employee.Email = updatedEmployee.Email;
            employee.PhoneNumber = updatedEmployee.PhoneNumber;
            employee.Address = updatedEmployee.Address;
            employee.DepartmentID = updatedEmployee.DepartmentID;
            employee.RoleID = updatedEmployee.RoleID;
            employee.Photo = updatedEmployee.Photo;

            await _context.SaveChangesAsync();
            return NoContent();
        }



        [HttpGet("roles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

    }
}

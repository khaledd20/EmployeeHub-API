using EmployeeHub.API.Data;
using EmployeeHub.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly EmployeeHubDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(EmployeeHubDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Fetch the employee by username only, avoid putting non-translatable methods in the query
            var employee = await _context.Employees
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Username == request.Username);

            // Perform the password check after retrieving the employee
            if (employee == null || !VerifyPassword(request.Password, employee.Password))
                return Unauthorized("Invalid username or password");

            var token = GenerateJwtToken(employee);
            return Ok(new { token, role = employee.Role?.RoleName, employeeId = employee.EmployeeID });
        }

        private string GenerateJwtToken(Employee employee)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, employee.Username),
                new Claim(ClaimTypes.NameIdentifier, employee.EmployeeID.ToString()), // Keep this if other systems rely on NameIdentifier
                new Claim("EmployeeID", employee.EmployeeID.ToString()), // Adding explicit EmployeeID claim
                new Claim(ClaimTypes.Role, employee.Role?.RoleID.ToString() ?? "0") // Use string for RoleID with a default value
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            // Implement password verification logic here, ideally using hashing
            return inputPassword == storedPassword; // Placeholder: replace with real hashing comparison
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

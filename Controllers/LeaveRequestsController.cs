using EmployeeHub.API.Data;
using EmployeeHub.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly EmployeeHubDbContext _context;

        public LeaveRequestsController(EmployeeHubDbContext context)
        {
            _context = context;
        }

        // GET: api/LeaveRequests
        [HttpGet]
        public async Task<IActionResult> GetLeaveRequests()
        {
            var requests = await _context.LeaveRequests.ToListAsync();
            return Ok(requests);
        }

        // GET: api/LeaveRequests/{leaveRequestID}
        [HttpGet("{leaveRequestID}")]
        public async Task<IActionResult> GetLeaveRequest(int leaveRequestID)
        {
            var leaveRequest = await _context.LeaveRequests
                .FirstOrDefaultAsync(lr => lr.LeaveRequestID == leaveRequestID);

            if (leaveRequest == null)
                return NotFound();

            return Ok(leaveRequest);
        }

        // POST: api/LeaveRequests
        [HttpPost]
        public async Task<IActionResult> PostLeaveRequest([FromBody] LeaveRequest leaveRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.LeaveRequests.Add(leaveRequest);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetLeaveRequest), new { leaveRequestID = leaveRequest.LeaveRequestID }, leaveRequest);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Database update error: " + ex.InnerException?.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // PUT: api/LeaveRequests/{leaveRequestID}
        [HttpPut("{leaveRequestID}")]
        public async Task<IActionResult> PutLeaveRequest(int leaveRequestID, [FromBody] LeaveRequest leaveRequest)
        {
            if (leaveRequestID != leaveRequest.LeaveRequestID)
                return BadRequest();

            _context.Entry(leaveRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.LeaveRequests.Any(lr => lr.LeaveRequestID == leaveRequestID))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/LeaveRequests/{leaveRequestID}
        [HttpDelete("{leaveRequestID}")]
        public async Task<IActionResult> DeleteLeaveRequest(int leaveRequestID)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestID);
            if (leaveRequest == null)
                return NotFound();

            _context.LeaveRequests.Remove(leaveRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

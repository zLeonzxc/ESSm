using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESSmAPI.Data;
using ESSmAPI.Models;

namespace ESSmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{empid}")]
        public async Task<ActionResult<Employee>> FindEmployee(string empid)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == empid);

            if (employee == null)
            {
                return NotFound();
            }

            var fullEmployee = await _context.Employees.FindAsync(employee.Id);

            if (fullEmployee == null)
            {
                return NotFound();
            }

            return fullEmployee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{empid}")]
        public async Task<IActionResult> EditEmployee(string empid, Employee employee)
        {
            if (empid != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(empid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
            return CreatedAtAction(nameof(FindEmployee), new { empid = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        // DELETE: api/Employees/5
        [HttpDelete("{empid}")]
        public async Task<IActionResult> DeleteEmployee(string empid)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == empid);
            if (employee == null)
            {
                return NotFound();
            }

            var fullEmployee = await _context.Employees.FindAsync(employee.Id);

            if (fullEmployee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(fullEmployee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(string empid)
        {
            return _context.Employees.Any(e => e.EmployeeId == empid);
        }
    }
}

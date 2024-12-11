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

            if (!_context.Employees.Any())
            {
                _context.Employees.AddRange(
                    new Employee { EmployeeId = "MY001", Name = "John", LegalName = "John Smith", Username = "jsmith", Email = "jsmith@example.com", Department = "IT", Country = "MY"},
                    new Employee { EmployeeId = "MY002", Name = "Jane", LegalName = "Jane Doe", Username = "janedoe", Email = "janedoe@example.com", Department = "IT", Country = "MY" },
                    new Employee { EmployeeId = "MY003", Name = "Ben", LegalName = "Benjamin Tan", Username = "benjamin", Email = "benjamin@example.com", Department = "IT", Country = "MY" },
                    new Employee { EmployeeId = "MY004", Name = "Henry", LegalName = "Henry Tan", Username = "henrytan", Email = "henrytan@example.com", Department = "IT", Country = "MY" },
                    new Employee { EmployeeId = "MY005", Name = "Dave", LegalName = "David Cross", Username = "dcross", Email = "dcross@example.com", Department = "IT", Country = "MY" },
                    new Employee { EmployeeId = "MY006", Name = "Ali", LegalName = "Muhammad Ali", Username = "muhammadali", Email = "muhammadali@example.com", Department = "IT", Country = "MY" },
                    new Employee { EmployeeId = "MY007", Name = "Ahmad", LegalName = "Daniel Ahmad", Username = "danielahmad", Email = "danielahmad@example.com", Department = "IT", Country = "MY" },
                    new Employee { EmployeeId = "MY008", Name = "Siti", LegalName = "Siti Maisarah", Username = "smaisarah", Email = "smaisarah@example.com", Department = "IT", Country = "MY" },
                    new Employee { EmployeeId = "MY009", Name = "Tamil", LegalName = "Tamil Murugan", Username = "tmurugan", Email = "tmurugan@example.com", Department = "IT", Country = "MY" },
                    new Employee { EmployeeId = "MY010", Name = "Raj", LegalName = "Rajesh Sarveen", Username = "rajveen", Email = "rajveen@example.com", Department = "IT", Country = "MY" }
                );
                _context.SaveChanges();
            }
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

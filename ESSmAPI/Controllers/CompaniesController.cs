using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESSmAPI.Data;
using ESSmAPI.Models;

namespace ESSmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly CompanyContext _context;

        public CompaniesController(CompanyContext context)
        {
            _context = context;

            if (!_context.Companies.Any())
            {
                _context.Companies.AddRange(
                    new Company { CompanyCode = "AB", CompanyName = "ABC" },
                    new Company { CompanyCode = "XY", CompanyName = "XYZ" },
                    new Company { CompanyCode = "QW", CompanyName = "QWE" },
                    new Company { CompanyCode = "", CompanyName = "Unregistered" },
                    new Company { CompanyCode = "MB", CompanyName = "Mercedes Benz" }
                );
                _context.SaveChanges();
            }
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        // GET: api/Companies/5
        [HttpGet("{companyCode}")]
        public async Task<ActionResult<Company>> GetCompany(string companyCode)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.CompanyCode == companyCode);
            if (company == null)
            {
                return NotFound();
            }
            return company;
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{companyCode}")]
        public async Task<IActionResult> PutCompany(string companyCode, Company company)
        {
            if (companyCode != company.CompanyCode)
            {
                return BadRequest();
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(companyCode))
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

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(string companyCode)
        {
            var company = await _context.Companies.FindAsync(companyCode);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(string companyCode)
        {
            return _context.Companies.Any(e => e.CompanyCode == companyCode);
        }
    }
}

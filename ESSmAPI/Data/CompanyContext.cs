using Microsoft.EntityFrameworkCore;
using ESSmAPI.Models;

namespace ESSmAPI.Data
{
    public class CompanyContext(DbContextOptions<CompanyContext> options) : DbContext(options)
    {
        public DbSet<Company> Companies { get; set; }

    }
}

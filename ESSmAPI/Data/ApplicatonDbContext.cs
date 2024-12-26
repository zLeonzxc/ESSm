using Microsoft.EntityFrameworkCore;
using ESSmAPI.Models;

namespace ESSmAPI.Data
{
    public class ApplicationDbContext(DbContextOptions<CompanyContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserDevice> UserDevice { get; set; }

    }
}

using ESSmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ESSmAPI.Data
{
    public class EmployeeContext(DbContextOptions<EmployeeContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }

    }
}

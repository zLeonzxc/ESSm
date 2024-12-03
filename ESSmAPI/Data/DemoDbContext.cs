using ESSmAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESSmAPI.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<User> Users { get; set; }

    }
}

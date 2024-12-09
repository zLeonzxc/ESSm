using ESSmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ESSmAPI.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<User> Users { get; set; }

    }
}

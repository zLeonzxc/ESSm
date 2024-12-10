using ESSmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ESSmAPI.Data
{
    public class UserContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

    }
}

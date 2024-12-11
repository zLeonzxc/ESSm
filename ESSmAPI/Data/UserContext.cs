using ESSmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ESSmAPI.Data
{
    public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

    }
}

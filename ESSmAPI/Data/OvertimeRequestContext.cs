using Microsoft.EntityFrameworkCore;
using ESSmAPI.Models;

namespace ESSmAPI.Data
{
    public class OvertimeRequestContext(DbContextOptions<OvertimeRequestContext> options) : DbContext(options)
    {
        public DbSet<OvertimeRequest> OvertimeRequests { get; set; }

    }
}

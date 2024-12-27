using Microsoft.EntityFrameworkCore;
using ESSmAPI.Models;

namespace ESSmAPI.Data
{
    public class LeaveRequestContext(DbContextOptions<LeaveRequestContext> options) : DbContext(options)
    {
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}

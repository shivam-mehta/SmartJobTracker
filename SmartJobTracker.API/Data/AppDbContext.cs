using Microsoft.EntityFrameworkCore;
using SmartJobTracker.API.Models;

namespace SmartJobTracker.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using _0API.Models;

namespace _0API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
    }
}

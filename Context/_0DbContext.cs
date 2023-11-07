using _0API.Models;
using Microsoft.EntityFrameworkCore;

namespace _0API.Context
{
    public class _0DbContext : DbContext
    {
        public DbSet<user> users { get; set; }
        public DbSet<stat> stats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<user>().HasIndex(u => u.hwid).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=162.19.227.17;Database=daio;Uid=admin;Pwd=UEApKrfyAq9GubJVkGSf;", new MySqlServerVersion(new Version(10, 5, 18)));
        }
    }
}

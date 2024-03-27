using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ElevatorDb");
        }

        public DbSet<Elevator> Elevators { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<State> States { get; set; }
    }
}

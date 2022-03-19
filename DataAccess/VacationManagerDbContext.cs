using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class VacationManagerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Vacation> Vacations { get; set; }

        public VacationManagerDbContext(DbContextOptions<VacationManagerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasOne(u => u.Team)
               .WithMany(t => t.Developers);
        }
    }
}

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
                .HasOne(u => u.Role);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Team)
                .WithMany(t => t.Developers)
                .HasForeignKey(u => u.TeamId);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Teams)
                .HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<Vacation>()
                .HasOne(v => v.Applicant);

            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = "1",
                    Name = "CEO"
                },
                new Role()
                {
                    Id = "2",
                    Name = "Developer"
                },
                new Role()
                {
                    Id = "3",
                    Name = "Team Lead"
                },
                new Role()
                {
                    Id = "4",
                    Name = "Unassigned"
                });
        }
    }
}

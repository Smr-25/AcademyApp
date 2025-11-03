using AcademyApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.DLL.Data;

public class AcademyDbContext : DbContext
{
    public DbSet<Group> Groups { get; set; }
    
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.;Database=AcademyAppDb;Trusted_Connection=True;TrustServerCertificate=True"
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AcademyDbContext).Assembly);
    }
}
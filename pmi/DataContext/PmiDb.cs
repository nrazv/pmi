using Microsoft.EntityFrameworkCore;
using pmi.Project.Models;


namespace pmi.DataContext;

public class PmiDb : DbContext
{
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ProjectInfo> ProjectsInfo { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string databaseFile = "db/pmi.db";
        string path = Path.Combine(Environment.CurrentDirectory, databaseFile);
        string connectionString = $"Data Source={path}";
        Console.WriteLine($"Connection: {connectionString}");
        optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
           .Entity<ProjectInfo>()
           .Property(e => e.Status)
           .HasConversion(
            v => v.ToString(),
            v => (ProjectStatus)Enum.Parse(typeof(ProjectStatus), v)
       );
    }
}
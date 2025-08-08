using Microsoft.EntityFrameworkCore;
using pmi.ExecutedTool.Models;
using pmi.Project.Models;


namespace pmi.Data;

public class PmiDbContext : DbContext
{
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ProjectInfo> ProjectsInfo { get; set; }
    public DbSet<ExecutedToolEntity> ExecutedTools { get; set; }


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

        modelBuilder.Entity<ExecutedToolEntity>()
            .Property(e => e.Status)
            .HasConversion(
                v => v.ToString(),
                v => (ExecutionStatus)Enum.Parse(typeof(ExecutionStatus), v)
            );
    }
}
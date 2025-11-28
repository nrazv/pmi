using Microsoft.EntityFrameworkCore;
using pmi.ExecutedTool.Models;
using pmi.ModelsConfiguration;
using pmi.Project.Models;


namespace pmi.Data;

public class PmiDbContext : DbContext
{
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ProjectInfo> ProjectsInfo { get; set; }
    public DbSet<ExecutedToolEntity> ExecutedTools { get; set; }


    public PmiDbContext(DbContextOptions<PmiDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProjectEntityConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}


using Microsoft.EntityFrameworkCore;
using pmi.ExecutedTool.Models;
using pmi.ModelsConfiguration;
using pmi.Modules.Models;
using pmi.Project.Models;


namespace pmi.Data;

public class PmiDbContext : DbContext
{
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ProjectInfo> ProjectsInfo { get; set; }
    public DbSet<ExecutedToolEntity> ExecutedTools { get; set; }
    public DbSet<ExecutedModuleEntity> ExecutedModules { get; set; }
    public DbSet<ModuleEntity> Modules { get; set; }


    public PmiDbContext(DbContextOptions<PmiDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ExecutedToolEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ModulesEntityConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}

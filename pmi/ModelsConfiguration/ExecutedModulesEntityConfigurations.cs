using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pmi.Modules.Models;

namespace pmi.ModelsConfiguration;


public class ExecutedModulesEntityConfiguration : IEntityTypeConfiguration<ExecutedModuleEntity>
{
    public void Configure(EntityTypeBuilder<ExecutedModuleEntity> builder)
    {
        builder.Navigation(e => e.ExecutedTools).AutoInclude();

        builder.HasOne(me => me.Project)
        .WithMany(p => p.ExecutedModules)
        .HasForeignKey(me => me.ProjectId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pmi.ExecutedTool.Models;

namespace pmi.ModelsConfiguration;

public class ExecutedToolEntityConfiguration : IEntityTypeConfiguration<ExecutedToolEntity>
{
    public void Configure(EntityTypeBuilder<ExecutedToolEntity> builder)
    {
        builder
         .Property(e => e.Status)
         .HasConversion(
             v => v.ToString(),
             v => (ExecutionStatus)Enum.Parse(typeof(ExecutionStatus), v!)
         );

        builder
            .HasOne(et => et.Project)
            .WithMany(p => p.ExecutedTools)
            .HasForeignKey(et => et.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(et => et.ExecutedModule)
            .WithMany(em => em.ExecutedTools)
            .HasForeignKey(et => et.ExecutedModuleId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pmi.Tool.Models;

namespace pmi.ModelsConfiguration;

public class ExecutableToolsConfiguration : IEntityTypeConfiguration<ExecutableTool>
{
    public void Configure(EntityTypeBuilder<ExecutableTool> builder)
    {
        builder.HasOne(et => et.ModuleEntity)
        .WithMany(m => m.ExecutablesTools)
        .HasForeignKey(et => et.ModuleEntityId);
    }
}

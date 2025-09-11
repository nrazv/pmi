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
    }
}
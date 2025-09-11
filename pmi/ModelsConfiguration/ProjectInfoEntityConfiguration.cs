using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pmi.Project.Models;

namespace pmi.ModelsConfiguration;

public class ProjectInfoEntityConfiguration : IEntityTypeConfiguration<ProjectInfo>
{
    public void Configure(EntityTypeBuilder<ProjectInfo> builder)
    {
        builder.Property(e => e.Status)
           .HasConversion(
            v => v.ToString(),
            v => (ProjectStatus)Enum.Parse(typeof(ProjectStatus), v));
    }
}
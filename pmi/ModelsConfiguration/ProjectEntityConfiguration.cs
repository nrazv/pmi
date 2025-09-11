using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pmi.Project.Models;

namespace pmi.ModelsConfiguration;

public class ProjectEntityConfiguration : IEntityTypeConfiguration<ProjectEntity>
{
    public void Configure(EntityTypeBuilder<ProjectEntity> builder)
    {
        builder.Navigation(e => e.ProjectInfo).AutoInclude();
        builder.HasOne(e => e.ProjectInfo)
               .WithOne(e => e.Project)
               .HasForeignKey<ProjectInfo>(e => e.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
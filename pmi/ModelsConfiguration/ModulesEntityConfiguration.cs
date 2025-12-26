using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pmi.Modules.Models;

namespace pmi.ModelsConfiguration;

public class ModulesEntityConfiguration : IEntityTypeConfiguration<ModuleEntity>
{
    public void Configure(EntityTypeBuilder<ModuleEntity> builder)
    {
        builder.Navigation(e => e.ExecutablesTools).AutoInclude();
    }
}
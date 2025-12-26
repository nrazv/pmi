using pmi.Data;
using pmi.Modules.Models;
using pmi.Repository;

namespace pmi.Modules.Repository;

public class ModuleRepository : Repository<ModuleEntity>, IModuleRepository
{
    private readonly PmiDbContext dbContext;
    public ModuleRepository(PmiDbContext pmiDbContext) : base(pmiDbContext)
    {
        dbContext = pmiDbContext;
    }

    public async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }
}
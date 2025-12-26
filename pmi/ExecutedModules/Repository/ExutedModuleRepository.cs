using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using pmi.Data;
using pmi.Modules.Models;
using pmi.Repository;

namespace pmi.ExecutedModule.Repository;

public class ExecutedModuleRepository : Repository<ExecutedModuleEntity>, IExecutedModuleRepository
{
    private readonly PmiDbContext dbContext;
    public ExecutedModuleRepository(PmiDbContext pmiDbContext) : base(pmiDbContext)
    {
        dbContext = pmiDbContext;
    }

    public async Task<List<ExecutedModuleEntity>> GetExecutedModulesAsync()
    {
        return await dbContext.ExecutedModules.Include(e => e.ExecutedTools).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public override async Task<IEnumerable<ExecutedModuleEntity>> GetAllWhere(
        Expression<Func<ExecutedModuleEntity, bool>> filter)
    {
        return await dbContext.ExecutedModules.Include(e => e.ExecutedTools).Where(filter).ToListAsync();
    }

}
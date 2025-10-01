using pmi.Data;
using pmi.ExecutedTool.Models;
using pmi.Repository;

namespace pmi.ExecutedTool.Repository;

public class ExecutedToolRepository : Repository<ExecutedToolEntity>, IExecutedToolRepository
{
    private readonly PmiDbContext dbContext;
    public ExecutedToolRepository(PmiDbContext pmiDbContext) : base(pmiDbContext)
    {
        dbContext = pmiDbContext;
    }

    public async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<ExecutedToolEntity> Update(ExecutedToolEntity obj)
    {
        dbContext.Update(obj);
        await dbContext.SaveChangesAsync();
        return obj;
    }

}
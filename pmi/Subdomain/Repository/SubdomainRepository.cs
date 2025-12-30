using pmi.Data;
using pmi.Repository;
using pmi.Subdomain.Models;

namespace pmi.Subdomain.Repository;

public class SubdomainRepository : Repository<SubdomainEntity>, ISubdomainRepository
{
    private readonly PmiDbContext dbContext;
    public SubdomainRepository(PmiDbContext pmiDbContext) : base(pmiDbContext)
    {
        dbContext = pmiDbContext;
    }

    public async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }
}
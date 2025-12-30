using pmi.Repository;
using pmi.Subdomain.Models;

namespace pmi.Subdomain.Repository;

public interface ISubdomainRepository : IRepository<SubdomainEntity>
{
    Task Save();
}
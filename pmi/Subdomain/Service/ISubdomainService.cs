using pmi.Service;
using pmi.Subdomain.Models;

namespace pmi.Subdomain.Service;


public interface ISubdomainService : IService<SubdomainEntity>
{
    Task SaveChangesAsync();
}
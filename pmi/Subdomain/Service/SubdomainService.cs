using pmi.Subdomain.Models;
using pmi.Subdomain.Repository;

namespace pmi.Subdomain.Service;

public class SubdomainService : ISubdomainService
{
    private readonly ISubdomainRepository repository;

    public SubdomainService(ISubdomainRepository repository)
    {
        this.repository = repository;
    }

    public async Task AddNew(SubdomainEntity obj)
    {
        await repository.Add(obj);
        await repository.Save();
    }

    public async Task<List<SubdomainEntity>> GetAll()
    {
        var list = await repository.GetAll();
        return list.ToList();
    }

    public async Task<SubdomainEntity?> GetById(string id)
    {
        return await repository.Get(e => e.Id.ToString() == id);
    }

    public async Task<SubdomainEntity?> GetByName(string name)
    {
        return await repository.Get(e => e.Domain == name);
    }

    public async Task SaveChangesAsync()
    {
        await repository.Save();
    }

    public SubdomainEntity Update(SubdomainEntity obj)
    {
        throw new NotImplementedException();
    }
}
using pmi.ExecutedTool.Models;

namespace pmi.ExecutedTool.Service;

public class ExecutedToolService : IExecutedToolService
{

    private readonly IExecutedToolRepository repository;

    public ExecutedToolService(IExecutedToolRepository repository)
    {
        this.repository = repository;
    }

    public async Task AddNew(ExecutedToolEntity obj)
    {
        await repository.Add(obj);
    }

    public Task<List<ExecutedToolEntity>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<ExecutedToolEntity?> GetById(string id)
    {
        return await repository.Get(e => e.Id == id);
    }

    public async Task<ExecutedToolEntity?> GetByName(string name)
    {
        return await repository.Get(e => e.Name == name);
    }

    public ExecutedToolEntity Update(ExecutedToolEntity obj)
    {
        throw new NotImplementedException();
    }
}
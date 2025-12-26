using pmi.ExecutedModule.Repository;
using pmi.Modules.Models;

namespace pmi.ExecutedModule.Service;

public class ExecutedModuleService : IExecutedModuleService
{
    private readonly IExecutedModuleRepository repository;

    public ExecutedModuleService(IExecutedModuleRepository repository)
    {
        this.repository = repository;
    }

    public async Task AddNew(ExecutedModuleEntity obj)
    {
        await repository.Add(obj);
        await repository.SaveChangesAsync();
    }

    public async Task<List<ExecutedModuleEntity>> GetAll()
    {
        return await repository.GetExecutedModulesAsync();
    }

    public Task<ExecutedModuleEntity?> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<ExecutedModuleEntity?> GetByName(string name)
    {
        return await repository.Get(e => e.Name == name);
    }

    public async Task<List<ExecutedModuleEntity>> GetExecutedModulesByProjectName(string projectName)
    {
        var response = await repository.GetAllWhere(m => m.Project.Name == projectName);
        return response.ToList();
    }

    public ExecutedModuleEntity Update(ExecutedModuleEntity obj)
    {
        throw new NotImplementedException();
    }
}
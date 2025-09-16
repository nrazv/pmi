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
        await repository.Save();
    }

    public Task<List<ExecutedToolEntity>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<List<ExecutedToolEntity>> GetAllByProjectName(string projectName)
    {
        var tools = await repository.GetAllWhere(p => p.Project.Name == projectName);
        return tools.ToList();
    }

    public async Task<ExecutedToolEntity?> GetById(string id)
    {
        return await repository.Get(e => e.Id.Equals(id));
    }

    public async Task<ExecutedToolEntity?> GetByName(string name)
    {
        return await repository.Get(e => e.Name == name);
    }

    public ExecutedToolEntity Update(ExecutedToolEntity obj)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateExecutedToolOutput(string toolId, string output)
    {
        var all = await repository.GetAll();

        var tool = await repository.Get(t => t.Id.ToString() == toolId);
        if (tool is null) { return; }
        tool.ExecutionResult = $"{tool.ExecutionResult}\n {output}";
        await repository.Add(tool);
        await repository.Save();
    }
}
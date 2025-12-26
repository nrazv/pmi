using pmi.Modules.Dto;
using pmi.Modules.Models;
using pmi.Modules.Repository;
using pmi.Tool.Models;
using pmi.Utilities;

namespace pmi.Modules.Service;

public class ModuleService : IModuleService
{
    private readonly IModuleRepository repository;

    public ModuleService(IModuleRepository repository)
    {
        this.repository = repository;
    }

    public Task AddNew(ModuleEntity obj)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ModuleEntity>> GetAll()
    {
        var module = await repository.GetAll();
        return module.ToList();
    }

    public Task<ModuleEntity?> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<ModuleEntity?> GetByName(string name)
    {
        return await repository.Get(m => m.Name == name);
    }

    public async Task<OperationResult<ModuleEntity>> NewModule(CreateModuleDto dto)
    {
        var module = await repository.Get(module => module.Name == dto.Name);

        if (module is ModuleEntity)
        {
            return OperationResult<ModuleEntity>.Failure("A module with the same name already exists.");
        }

        List<ExecutableTool> executableTools = new();
        ModuleEntity moduleEntity = new ModuleEntity
        {
            Name = dto.Name,
            Description = dto.Description,
            ExecutablesTools = executableTools
        };



        foreach (ExecutableToolDto tool in dto.ExecutableTools)
        {
            executableTools.Add(
                new ExecutableTool
                {
                    Id = Guid.NewGuid(),
                    Name = tool.Name,
                    Arguments = tool.Arguments,
                    ModuleEntity = moduleEntity
                });
        }

        await repository.Add(moduleEntity);
        await repository.Save();

        return OperationResult<ModuleEntity>.Successful(moduleEntity);
    }

    public ModuleEntity Update(ModuleEntity obj)
    {
        throw new NotImplementedException();
    }
}
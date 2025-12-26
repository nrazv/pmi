using pmi.Modules.Models;
using pmi.Service;

namespace pmi.ExecutedModule.Service;

public interface IExecutedModuleService : IService<ExecutedModuleEntity>
{

    Task<List<ExecutedModuleEntity>> GetExecutedModulesByProjectName(string projectName);
}
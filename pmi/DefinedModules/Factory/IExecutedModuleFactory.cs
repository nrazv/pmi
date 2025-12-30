using pmi.DefinedModules.Models;
using pmi.Modules.Models;
using pmi.Project.Models;

namespace pmi.DefinedModules.Factory;

public interface IExecutedModuleFactory
{
    ExecutedModuleEntity CreateExecutedModule(ModuleEntity moduleEntity, ModuleExecutionRequest request, ProjectEntity project);
}
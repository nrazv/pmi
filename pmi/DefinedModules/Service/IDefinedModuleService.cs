using pmi.DefinedModules.Models;

namespace pmi.DefinedModules.Services;


public interface IDefinedModuleService
{
    Task ExecuteModuleAsync(ModuleExecutionRequest request);
}
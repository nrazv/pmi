using pmi.DefinedModules.CRT.Models;

namespace pmi.DefinedModules.Services;


public interface IDefinedModuleService
{
    Task ExecuteModuleAsync(ModuleExecutionRequest request, Guid id);
}
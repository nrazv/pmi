using pmi.Data;
using pmi.Modules.Models;
using pmi.Repository;

namespace pmi.ExecutedModule.Repository;

public interface IExecutedModuleRepository : IRepository<ExecutedModuleEntity>
{
    Task SaveChangesAsync();
    Task<List<ExecutedModuleEntity>> GetExecutedModulesAsync();
}
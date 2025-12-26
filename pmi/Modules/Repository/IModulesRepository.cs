using pmi.Modules.Models;
using pmi.Repository;

namespace pmi.Modules.Repository;

public interface IModuleRepository : IRepository<ModuleEntity>
{
    Task Save();
}
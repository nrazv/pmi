using pmi.Modules.Dto;
using pmi.Modules.Models;
using pmi.Service;
using pmi.Utilities;

namespace pmi.Modules.Service;

public interface IModuleService : IService<ModuleEntity>
{
    public Task<OperationResult<ModuleEntity>> NewModule(CreateModuleDto dto);

}
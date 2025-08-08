using pmi.ExecutedTool.Models;
using pmi.Repository;

namespace pmi.ExecutedTool;

public interface IExecutedToolRepository : IRepository<ExecutedToolEntity>
{
    Task<ExecutedToolEntity> Update(ExecutedToolEntity obj);
    Task Save();
}
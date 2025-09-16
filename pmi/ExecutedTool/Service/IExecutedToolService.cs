using pmi.ExecutedTool.Models;
using pmi.Service;

namespace pmi.ExecutedTool.Service;

public interface IExecutedToolService : IService<ExecutedToolEntity>
{
    public Task UpdateExecutedToolOutput(string toolId, string output);
    public Task<List<ExecutedToolEntity>> GetAllByProjectName(string projectName);

}
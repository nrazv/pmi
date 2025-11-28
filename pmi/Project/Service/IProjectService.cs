using pmi.ExecutedTool.Models;
using pmi.Project.Models;
using pmi.Utilities;
namespace pmi.Project.Service;

public interface IProjectService
{
    public Task<List<ProjectEntity>> GetProjects();
    public Task<OperationResult<ProjectEntity>> NewProject(CreateProjectDto project);
    public Task AddExecutedTool(string projectId, ExecutedToolEntity executedTool);
    public Task<ProjectEntity?> GetById(string id);
    public Task<ProjectEntity?> GetByName(string name);
    public Task<ProjectEntity?> SearchByName(string name);

    public ExecutedToolEntity UpdateExecutedToo(ExecutedToolEntity executedTool);
    public ExecutedToolEntity? GetExecutedTooById(string id);
    public Task AddNewExecutedTool(ExecutedToolEntity executedTool);

    public List<ExecutedToolEntity> GetExecutedToolEntitiesByProjectName(string projectName);

    public Task<OperationResult<string>> DeleteById(Guid id);
}

using pmi.ExecutedTool.Models;
using pmi.Project.Models;
namespace pmi.Project.Services;

public interface IProjectService
{
    public Task<List<ProjectEntity>> GetProjects();
    public Task<ProjectEntity> NewProject(CreateProjectDto project);
    public Task AddExecutedTool(string projectId, ExecutedToolEntity executedTool);
    public Task<ProjectEntity?> GetById(string id);
    public Task<ProjectEntity?> GetByName(string name);

    public ExecutedToolEntity UpdateExecutedToo(ExecutedToolEntity executedTool);
    public ExecutedToolEntity? GetExecutedTooById(string id);
    public void AddNewExecutedTool(ExecutedToolEntity executedTool);

    public List<ExecutedToolEntity> GetExecutedToolEntitiesByProjectName(string projectName);

}

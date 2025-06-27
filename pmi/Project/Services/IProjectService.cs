using pmi.Project.Models;
namespace pmi.Project.Services;

public interface IProjectService
{
    public List<ProjectDto> GetProjects();
    public (ProjectDto?, string? errorMessage) NewProject(CreateProjectDto project);
    public ProjectEntity AddExecutedTool(string projectId, ExecutedToolEntity executedTool);
    public ProjectEntity GetById(string id);
    public ProjectEntity GetByName(string name);

    public ExecutedToolEntity UpdateExecutedToo(ExecutedToolEntity executedTool);
    public ExecutedToolEntity? GetExecutedTooById(string id);
    public void AddNewExecutedTool(ExecutedToolEntity executedTool);

    public List<ExecutedToolEntity> GetExecutedToolEntitiesByProjectName(string projectName);

}

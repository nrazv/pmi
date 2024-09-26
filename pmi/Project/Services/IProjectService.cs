using Microsoft.AspNetCore.Mvc;
using pmi.Project.Models;
namespace pmi.Project.Services;

public interface IProjectService
{
    public List<ProjectDto> GetProjects();
    public (ProjectDto?, string? errorMessage) NewProject(string projectName);
}

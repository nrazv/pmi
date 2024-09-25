using Microsoft.AspNetCore.Mvc;
using pmi.Project.Models;
namespace pmi.Project.Services;

public interface IProjectService
{
    public (ProjectEntity?, string? errorMessage) NewProject(string projectName);
    public ProjectEntity GetById(Guid id);
}

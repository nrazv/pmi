using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pmi.Project.Models;
using pmi.Project.Services;

namespace pmi.Project.Controller;

[ApiController]
[Route("api/project")]
public class ProjectController : ControllerBase
{
    IProjectService _projectService;
    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }


    [HttpPost("new", Name = "New Project")]
    [ProducesResponseType<ProjectDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task NewProject(CreateProjectDto project)
    {
        await _projectService.NewProject(project);
    }


    [HttpGet("all", Name = "Get all projects")]
    public async Task<List<ProjectDto>> GetAll()
    {
        return await _projectService.GetProjects();
    }

}

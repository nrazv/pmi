using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pmi.Project.Models;
using pmi.Project.Services;

namespace pmi.Project.Controller;

[ApiController]
[Route("api/project")]
public class ProjectController : ControllerBase
{
    IProjectService _projectService;
    IMapper _mapper;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
        _mapper = new Mapper(new MapperConfiguration(conf =>
        {
            conf.CreateMap<ProjectEntity, ProjectDto>();
        }));
    }

    [HttpPost("new", Name = "New Project")]
    [ProducesResponseType<ProjectDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult NewProject(string projectName)
    {
        var (project, errorMessage) = _projectService.NewProject(projectName);
        if (errorMessage is null)
        {
            ProjectDto projectDto = _mapper.Map<ProjectDto>(project);
            return Ok(projectDto);
        }

        return BadRequest(errorMessage);
    }


}

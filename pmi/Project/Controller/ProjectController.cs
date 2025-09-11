using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pmi.Project.Models;
using pmi.Project.Service;

namespace pmi.Project.Controller;

[ApiController]
[Route("api/project")]
public class ProjectController : ControllerBase
{
    IProjectService _projectService;
    private readonly IMapper _mapper;
    public ProjectController(IProjectService projectService, IConfiguration configuration)
    {
        _projectService = projectService;
        _mapper = new Mapper(new MapperConfiguration(conf =>
        {
            conf.CreateMap<ProjectInfo, ProjectInfoDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            conf.CreateMap<ProjectEntity, ProjectDto>();
        }));
    }


    [HttpPost("new", Name = "New Project")]
    [ProducesResponseType<ProjectDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> NewProject(CreateProjectDto project)
    {
        var result = await _projectService.NewProject(project);

        if (result.Success is false)
        {
            return BadRequest(new { message = result.ErrorMessage });
        }
        else
        {
            ProjectDto projectDto = _mapper.Map<ProjectDto>(result.Entity);
            return CreatedAtAction(nameof(NewProject), projectDto);
        }
    }


    [HttpGet("all", Name = "Get all projects")]
    public async Task<List<ProjectDto>> GetAll()
    {
        var response = await _projectService.GetProjects();
        return _mapper.Map<List<ProjectDto>>(response);
    }


    [HttpDelete("{Id}", Name = "Delete project by id")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProjectDto>(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteProject(Guid Id)
    {
        var result = await _projectService.DeleteById(Id);

        if (result.Success is false)
        {
            return BadRequest(new { message = result.ErrorMessage });
        }
        else
        {
            return NoContent();
        }
    }

}

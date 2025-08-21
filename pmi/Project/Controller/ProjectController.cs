using System.Threading.Tasks;
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
        if (string.IsNullOrEmpty(project.IpAddress) && string.IsNullOrEmpty(project.DomainName))
        {
            return BadRequest();
        }
        else
        {
            ProjectEntity newProject = await _projectService.NewProject(project);
            ProjectDto projectDto = _mapper.Map<ProjectDto>(newProject);
            return CreatedAtAction(nameof(NewProject), projectDto);
        }
    }


    [HttpGet("all", Name = "Get all projects")]
    public async Task<List<ProjectDto>> GetAll()
    {
        var response = await _projectService.GetProjects();
        return _mapper.Map<List<ProjectDto>>(response);

    }

}

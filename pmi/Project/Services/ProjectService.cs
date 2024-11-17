using AutoMapper;
using pmi.DataContext;
using pmi.Project.Models;

namespace pmi.Project.Services;

public class ProjectService : IProjectService
{
    private readonly ProjectManager _projectManager;
    private readonly PmiDb _pmiDb;
    private readonly IMapper _mapper;

    public ProjectService(IConfiguration configuration)
    {
        string projectFolder = configuration.GetSection("RootFolder").Value ?? "projects";
        _projectManager = new ProjectManager(projectFolder);
        _pmiDb = new PmiDb();
        _mapper = new Mapper(new MapperConfiguration(conf =>
        {
            conf.CreateMap<ProjectEntity, ProjectDto>();
        }));
    }

    public List<ProjectDto> GetProjects()
    {
        var projects = _projectManager.GetProjects();
        var dbProject = _pmiDb.Projects?.ToHashSet();

        return _mapper.Map<List<ProjectDto>>(dbProject);
    }

    public (ProjectDto?, string? errorMessage) NewProject(string projectName)
    {
        var project = _projectManager.createNewProject(projectName, out string? errorMessage);

        return (_mapper.Map<ProjectDto>(project), errorMessage);
    }
}

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
        var projects = _pmiDb.Projects?.ToHashSet();
        return _mapper.Map<List<ProjectDto>>(projects);
    }

    public (ProjectDto?, string? errorMessage) NewProject(CreateProjectDto p)
    {
        Guid uuid = Guid.NewGuid();


        ProjectEntity newProject = new(
                id: uuid.ToString(),
                name: p.Name,
                createdDate: DateTime.Now,
                domainName: p.DomainName ?? string.Empty,
                lastUpdated: DateTime.Now,
                ipAddress: p.IpAddress
                );
        _pmiDb.Add(newProject);
        _pmiDb.SaveChanges();
        var project = _projectManager.createNewProject(p.Name, out string? errorMessage);

        return (_mapper.Map<ProjectDto>(project), errorMessage);
    }
}

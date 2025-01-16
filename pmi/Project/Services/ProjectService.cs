using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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
        var projects = _pmiDb.Projects?.Include(p => p.ProjectInfo).ToHashSet();
        return _mapper.Map<List<ProjectDto>>(projects);
    }

    public (ProjectDto?, string? errorMessage) NewProject(CreateProjectDto p)
    {
        var newProjectEntity = createNewProjectEntity(p);
        _projectManager.createNewProjectDirectory(newProjectEntity.Name, out string? errorMessage);

        if (!string.IsNullOrEmpty(errorMessage))
        {
            return (null, errorMessage);
        }

        var (savedProject, error) = saveProjectToDatabase(newProjectEntity);
        if (string.IsNullOrEmpty(error) || savedProject is null)
        {
            return (null, error);
        }

        return (_mapper.Map<ProjectDto>(savedProject), errorMessage);
    }

    private (ProjectEntity?, string? errorMessage) saveProjectToDatabase(ProjectEntity newProject)
    {

        try
        {
            _pmiDb.Projects.Add(newProject);
            _pmiDb.SaveChanges();

        }
        catch (DbUpdateException ex) when (ex.InnerException is SqliteException sqliteEx && sqliteEx.SqliteErrorCode == 19)
        {
            _pmiDb.Remove(newProject);
            _pmiDb.SaveChanges();
            return (null, "Project exists");
        }

        var savedProject = _pmiDb.Projects?.OrderBy(p => p.Id).First();
        return (savedProject, null);
    }


    private ProjectEntity createNewProjectEntity(CreateProjectDto p)
    {
        var projectId = Guid.NewGuid().ToString();

        ProjectInfo projectInfo = new(
            id: Guid.NewGuid().ToString(),
            name: p.Name,
            createdDate: DateTime.Now,
            lastUpdated: DateTime.Now,
            status: ProjectStatus.NotStarted
            );

        return new(
                id: projectId,
                name: p.Name,
                domainName: p.DomainName ?? string.Empty,
                ipAddress: p.IpAddress,
                projectInfo: projectInfo
                );
    }
}

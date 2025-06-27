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
            conf.CreateMap<ProjectInfo, ProjectInfoDto>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));

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
        if (error is not null)
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

        var savedProject = _pmiDb.Projects?.Where(p => p.Name == newProject.Name).
                            Include(p => p.ProjectInfo).FirstOrDefault();
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

    public ProjectEntity AddExecutedTool(string projectId, ExecutedToolEntity executedTool)
    {
        var project = _pmiDb.Projects.Where(p => p.Id == projectId).First();
        project.ExecutedTools.Add(executedTool);
        _pmiDb.SaveChanges();
        return project;
    }

    public ProjectEntity GetById(string id)
    {
        return _pmiDb.Projects.Where(p => p.Id == id).First();
    }

    public ProjectEntity GetByName(string name)
    {
        return _pmiDb.Projects.Where(p => p.Name == name).First();
    }

    public ExecutedToolEntity UpdateExecutedToo(ExecutedToolEntity executedTool)
    {
        var tool = _pmiDb.ExecutedTools.Where((e) => e.Id == executedTool.Id).First();
        tool.ExecutionResult = executedTool.ExecutionResult;
        tool.Status = executedTool.Status;
        tool.FinishedDated = executedTool.FinishedDated;
        tool.ClientId = executedTool.ClientId;
        tool.RunnerId = executedTool.RunnerId;

        _pmiDb.SaveChanges();

        return tool;

    }

    public ExecutedToolEntity? GetExecutedTooById(string id)
    {
        try
        {
            return _pmiDb.ExecutedTools.Where((e) => e.Id == id).FirstOrDefault();

        }
        catch (Exception e)
        {

        }

        return null;
    }

    public void AddNewExecutedTool(ExecutedToolEntity executedTool)
    {
        _pmiDb.ExecutedTools.Add(executedTool);
        _pmiDb.SaveChanges();
    }

    public List<ExecutedToolEntity> GetExecutedToolEntitiesByProjectName(string projectName)
    {
        return _pmiDb.ExecutedTools.Include(et => et.Project)
                .Where(et => et.Project.Name == projectName)
                .ToList();
    }
}

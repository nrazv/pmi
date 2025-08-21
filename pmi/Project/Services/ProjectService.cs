using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pmi.Project.Models;
using pmi.Data;
using pmi.Project.Repository;
using pmi.Project.Builders;
using pmi.ExecutedTool.Models;
using pmi.ExecutedTool;

namespace pmi.Project.Services;

public class ProjectService : IProjectService
{
    private readonly PmiDbContext _pmiDb;
    private readonly IProjectRepository repository;
    private readonly IExecutedToolRepository executedToolRepository;
    private readonly IMapper _mapper;
    private readonly ProjectEntityBuilder entityBuilder;

    public ProjectService(IConfiguration configuration, IProjectRepository projectRepository, ProjectEntityBuilder entityBuilder, IExecutedToolRepository executedToolRepository)
    {
        this.entityBuilder = entityBuilder;
        this.executedToolRepository = executedToolRepository;
        repository = projectRepository;
        string projectFolder = configuration.GetSection("RootFolder").Value ?? "projects";
        _pmiDb = new PmiDbContext();
        _mapper = new Mapper(new MapperConfiguration(conf =>
        {
            conf.CreateMap<ProjectInfo, ProjectInfoDto>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));

            conf.CreateMap<ProjectEntity, ProjectDto>();
        }));
    }

    public async Task<List<ProjectEntity>> GetProjects()
    {
        var projects = await repository.GetAll();
        return projects.ToList();
    }

    public async Task<ProjectEntity> NewProject(CreateProjectDto p)
    {
        var newProjectEntity = entityBuilder.createNewProjectEntity(p);
        await repository.Add(newProjectEntity);
        await repository.Save();
        return newProjectEntity;
    }

    public async Task AddExecutedTool(string projectId, ExecutedToolEntity executedTool)
    {
        var project = await repository.Get(p => p.Id == projectId);
        project?.ExecutedTools.Add(executedTool);
        _pmiDb.SaveChanges();
    }

    public async Task<ProjectEntity?> GetById(string id)
    {
        return await repository.Get(p => p.Id == id);
    }

    public async Task<ProjectEntity?> GetByName(string name)
    {
        return await repository.Get(p => p.Name == name);
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
        executedToolRepository.Add(executedTool);

    }

    public List<ExecutedToolEntity> GetExecutedToolEntitiesByProjectName(string projectName)
    {
        return _pmiDb.ExecutedTools.Include(et => et.Project)
                .Where(et => et.Project.Name == projectName)
                .ToList();
    }
}

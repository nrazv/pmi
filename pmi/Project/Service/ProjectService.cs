using Microsoft.EntityFrameworkCore;
using pmi.Project.Models;
using pmi.Data;
using pmi.Project.Repository;
using pmi.ExecutedTool.Models;
using pmi.ExecutedTool;
using pmi.Utilities;

namespace pmi.Project.Service;

public class ProjectService : IProjectService
{
    private readonly PmiDbContext _pmiDb;
    private readonly IProjectRepository repository;
    private readonly IExecutedToolRepository executedToolRepository;

    public ProjectService(IProjectRepository projectRepository, IExecutedToolRepository executedToolRepository)
    {
        this.executedToolRepository = executedToolRepository;
        repository = projectRepository;
        _pmiDb = new PmiDbContext();
    }

    public async Task<List<ProjectEntity>> GetProjects()
    {
        var projects = await repository.GetAll();
        return projects.ToList();
    }

    public async Task<OperationResult<ProjectEntity>> NewProject(CreateProjectDto request)
    {
        if (string.IsNullOrEmpty(request.IpAddress) && string.IsNullOrEmpty(request.DomainName))
        {
            return OperationResult<ProjectEntity>.Failure("IpAddress or domainName is required.");
        }

        var project = await repository.Get(project => project.Name == request.Name);

        if (project is ProjectEntity)
        {
            return OperationResult<ProjectEntity>.Failure("A project with the same name already exists.");
        }

        ProjectEntity newProjectEntity = new ProjectEntity
        {
            Name = request.Name,
            DomainName = request.DomainName ?? string.Empty,
            IpAddress = request.IpAddress ?? string.Empty,
            ProjectInfo = new ProjectInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                CreatedDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                Status = ProjectStatus.NotStarted,

            }
        };

        await repository.Add(newProjectEntity);
        await repository.Save();

        return OperationResult<ProjectEntity>.Successful(newProjectEntity);
    }

    public async Task AddExecutedTool(string projectId, ExecutedToolEntity executedTool)
    {
        var project = await repository.Get(p => p.Id.Equals(p.Id));
        project?.ExecutedTools.Add(executedTool);
        _pmiDb.SaveChanges();
    }

    public async Task<ProjectEntity?> GetById(string id)
    {
        return await repository.Get(p => p.Id.Equals(p.Id));
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
            return _pmiDb.ExecutedTools.Where((e) => e.Id.Equals(id)).FirstOrDefault();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return null;
    }

    public async Task AddNewExecutedTool(ExecutedToolEntity executedTool)
    {
        await executedToolRepository.Add(executedTool);
    }

    public List<ExecutedToolEntity> GetExecutedToolEntitiesByProjectName(string projectName)
    {
        return _pmiDb.ExecutedTools.Include(et => et.Project)
                .Where(et => et.Project.Name == projectName)
                .ToList();
    }

    public async Task<OperationResult<string>> DeleteById(Guid id)
    {
        var project = await GetById(id.ToString());

        if (project is null)
        {
            return OperationResult<string>.Failure($"Resource with ID {id} not found.");
        }

        var response = await repository.Delete(project);

        if (response > 0)
        {
            return OperationResult<string>.Successful("Ok");
        }
        else
        {
            return OperationResult<string>.Failure($"Resource with ID {id} not found.");
        }
    }
}
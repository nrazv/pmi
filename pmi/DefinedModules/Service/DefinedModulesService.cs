using System.Threading.Channels;
using Microsoft.AspNetCore.SignalR;
using pmi.DefinedModules.BackgroundJob;
using pmi.DefinedModules.CRT;
using pmi.DefinedModules.Factory;
using pmi.DefinedModules.Models;
using pmi.DefinedModules.ModulesHub;
using pmi.ExecutedModule.Service;
using pmi.Modules.Models;
using pmi.Modules.Service;
using pmi.Project.Service;
using pmi.Tool.Models;


namespace pmi.DefinedModules.Services;

public class DefinedModuleService : IDefinedModuleService
{
    private readonly IModuleService _moduleService;
    private readonly IExecutedModuleFactory _executedModuleFactory;
    private readonly IExecutedModuleService _executedModuleService;
    private readonly IProjectService _projectService;
    private readonly Channel<IModuleBackgroundJob> _channel;
    private readonly IHubContext<DefinedModuleHub> _hub;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILoggerFactory _loggerFactory;


    public DefinedModuleService(
        IExecutedModuleFactory executedModuleFactory,
        IModuleService moduleService, IExecutedModuleService executedModuleService,
        IProjectService projectService, Channel<IModuleBackgroundJob> channel,
        IHubContext<DefinedModuleHub> hub,
        IServiceScopeFactory scopeFactory,
        ILoggerFactory loggerFactory
        )
    {
        _hub = hub;
        _channel = channel;
        _scopeFactory = scopeFactory;
        _loggerFactory = loggerFactory;
        _moduleService = moduleService;
        _projectService = projectService;
        _executedModuleFactory = executedModuleFactory;
        _executedModuleService = executedModuleService;

    }


    public async Task ExecuteModuleAsync(ModuleExecutionRequest request)
    {
        var moduleToExecute = await _moduleService.GetByName(request.ModuleName) ?? throw new InvalidOperationException($"Module '{request.ModuleName}' not found");
        var newModule = await PersistExecutedModule(moduleToExecute, request);

        foreach (var tool in newModule.ExecutedTools)
        {
            if (tool.Name == "crt")
            {
                CrtDomainEnumeration crtModuleJob = new CrtDomainEnumeration(
                    executionId: tool.Id, request: request, scopeFactory: _scopeFactory
                );
                _channel.Writer.TryWrite(crtModuleJob);
                continue;
            }

            var job = new ModuleProcessJob(executionId: tool.Id, hub: _hub, _scopeFactory, request: new ToolExecutionRequest
            {
                Target = tool.Target,
                Tool = tool.Name,
                Arguments = tool.ToolArguments,
                ProjectName = request.ProjectName,

            }, loggerFactory: _loggerFactory);
            _channel.Writer.TryWrite(job);
        }
    }

    private async Task<ExecutedModuleEntity> PersistExecutedModule(ModuleEntity moduleEntity, ModuleExecutionRequest request)
    {
        var project = await _projectService.GetByName(request.ProjectName) ?? throw new InvalidOperationException($"Project '{request.ModuleName}' not found");
        var NewExecutedModule = _executedModuleFactory.CreateExecutedModule(moduleEntity, request, project);
        await _executedModuleService.AddNew(NewExecutedModule);
        return NewExecutedModule;
    }
}
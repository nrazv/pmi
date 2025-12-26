
using Microsoft.AspNetCore.SignalR;
using pmi.DefinedModules.ModulesHub;
using pmi.ExecutedTool.Models;
using pmi.ExecutedTool.Service;
using pmi.Tool.Managers;
using pmi.Tool.Models;

namespace pmi.DefinedModules.BackgroundJob;

public class ModuleProcessJob : IModuleBackgroundJob
{
    public Guid ExecutionId { get; }
    private IHubContext<DefinedModuleHub> _hub;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ToolExecutionRequest _request;
    private readonly ProcessManager processManager;
    private readonly ILoggerFactory _loggerFactory;
    public ModuleProcessJob(Guid executionId, IHubContext<DefinedModuleHub> hub, IServiceScopeFactory scopeFactory, ToolExecutionRequest request, ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        _hub = hub;
        _request = request;
        ExecutionId = executionId;
        _scopeFactory = scopeFactory;
        processManager = new ProcessManager();
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var logger = _loggerFactory.CreateLogger("ToolExecution");
        var process = processManager.CreateNewProcess(_request);
        process.OutputDataReceived += async (s, e) =>
        {
            if (e.Data is not null)
            {
                using var eventScope = _scopeFactory.CreateScope();
                var executedToolService = eventScope.ServiceProvider.GetRequiredService<IExecutedToolService>();
                await _hub.Clients.Group(ExecutionId.ToString()).SendAsync("ExecutedModuleOutput", e.Data);
                await executedToolService.UpdateExecutedToolOutput(ExecutionId.ToString(), e.Data);
            }
        };


        process.Exited += async (s, e) =>
        {
            using var eventScope = _scopeFactory.CreateScope();
            var executedToolService = eventScope.ServiceProvider.GetRequiredService<IExecutedToolService>();
            if (process.ExitCode == 0)
            {

                await executedToolService.UpdateStatus(ExecutionId.ToString(), ExecutionStatus.Done);
            }
            else
            {
                await executedToolService.UpdateStatus(ExecutionId.ToString(), ExecutionStatus.Failed);
            }
        };

        try
        {
            using var eventScope = _scopeFactory.CreateScope();
            var executedToolService = eventScope.ServiceProvider.GetRequiredService<IExecutedToolService>();
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            await executedToolService.UpdateStatus(ExecutionId.ToString(), ExecutionStatus.Running);
            await process.WaitForExitAsync(cancellationToken);

        }
        catch (Exception e)
        {
            logger.LogError($"Process exception id: {ExecutionId}:", e.Data);
        }
        finally
        {

            process.CancelOutputRead();
            process.CancelErrorRead();
            process.Close();
            process.Dispose();
        }

    }
}

using System.Threading.Channels;
using Microsoft.AspNetCore.SignalR;
using pmi.ExecutedTool.Models;
using pmi.ExecutedTool.Service;
using pmi.Project.Models;
using pmi.Project.Service;
using pmi.Tool.Managers;
using pmi.Tool.Models;
using pmi.Utilities;

namespace pmi.Tool.Services;

public class ToolExecutionBackgroundService : BackgroundService
{
    private readonly Channel<ToolJob> _channel;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ProcessManager processManager;
    private readonly IHubContext<ToolHub> _hub;
    private readonly ILoggerFactory _loggerFactory;

    public ToolExecutionBackgroundService(IServiceScopeFactory scopeFactory, Channel<ToolJob> channel, IHubContext<ToolHub> hub, ILoggerFactory loggerFactory)
    {
        processManager = new ProcessManager();
        _scopeFactory = scopeFactory;
        _channel = channel;
        _hub = hub;
        _loggerFactory = loggerFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var reader = _channel.Reader;

        while (await reader.WaitToReadAsync(stoppingToken))
        {
            while (reader.TryRead(out var job))
            {
                _ = Task.Run(() => HandleJobAsync(job, stoppingToken), stoppingToken);
            }
        }
    }

    private async Task HandleJobAsync(ToolJob job, CancellationToken cancellationToken)
    {
        var logger = _loggerFactory.CreateLogger("ToolExecution");
        using var scope = _scopeFactory.CreateScope();
        var projectService = scope.ServiceProvider.GetRequiredService<IProjectService>();
        var executedToolService = scope.ServiceProvider.GetRequiredService<IExecutedToolService>();
        string executionId = job.ExecutionId.ToString();
        ProjectEntity? project = await projectService.GetByName(job.Request.ProjectName);

        if (project is null) { return; }
        var executedTool = ProjectFactory.CreateExecutedToolFromExecutionRequest(toolId: Guid.Parse(job.ExecutionId), request: job.Request, project: project!, runnerId: null);
        await executedToolService.AddNew(executedTool);

        logger.LogInformation($"\nExecution started for: {job.Request}");
        var process = processManager.CreateNewProcess(job.Request);

        process.OutputDataReceived += async (s, e) =>
        {
            if (e.Data is not null)
            {
                using var eventScope = _scopeFactory.CreateScope();
                var executedToolService = eventScope.ServiceProvider.GetRequiredService<IExecutedToolService>();
                await _hub.Clients.Group(executionId).SendAsync("ReceiveOutput", e.Data);
                await executedToolService.UpdateExecutedToolOutput(executionId, e.Data);
                logger.LogInformation($"\n ----------------------------\nOUTPUT{e.Data}\n--------------------\n");
            }
        };

        process.ErrorDataReceived += async (s, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                using var eventScope = _scopeFactory.CreateScope();
                var executedToolService = eventScope.ServiceProvider.GetRequiredService<IExecutedToolService>();
                await executedToolService.UpdateStatus(executionId, ExecutionStatus.Error);
                await _hub.Clients.Group(executionId).SendAsync("ReceiveError", e.Data);
            }
        };

        var exitTcs = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);

        process.Exited += async (s, e) =>
        {
            using var eventScope = _scopeFactory.CreateScope();
            var executedToolService = eventScope.ServiceProvider.GetRequiredService<IExecutedToolService>();
            await executedToolService.UpdateStatus(executionId, ExecutionStatus.Done);
            exitTcs.TrySetResult(process.ExitCode);
        };

        try
        {
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            // Wait asynchronously for process exit or cancellation
            using (cancellationToken.Register(() => exitTcs.TrySetCanceled()))
            {
                int exitCode = await exitTcs.Task; // WaitForExitAsync alternative
                await _hub.Clients.Group(executionId).SendAsync("StatusUpdate", ExecutionStatus.Done.ToString());
                await _hub.Clients.Group(executionId).SendAsync("Completed", new { executionId, exitCode });
            }
        }
        catch (OperationCanceledException)
        {
            logger.LogError($"\n =============== OperationCanceled =================");
            await _hub.Clients.Group(executionId).SendAsync("StatusUpdate", ExecutionStatus.Failed.ToString());
        }
        catch (Exception ex)
        {
            logger.LogError($"\n =============== Exception{ex.Message} =================");
            await _hub.Clients.Group(executionId).SendAsync("ReceiveError", $"Host exception: {ex.Message}");
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
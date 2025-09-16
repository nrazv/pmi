
using System.Threading.Channels;
using Microsoft.AspNetCore.SignalR;
using pmi.ExecutedTool.Models;
using pmi.ExecutedTool.Service;
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

    public ToolExecutionBackgroundService(IServiceScopeFactory scopeFactory, Channel<ToolJob> channel, IHubContext<ToolHub> hub)
    {
        processManager = new ProcessManager();
        _scopeFactory = scopeFactory;
        _channel = channel;
        _hub = hub;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var reader = _channel.Reader;

        while (await reader.WaitToReadAsync(stoppingToken))
        {
            while (reader.TryRead(out var job))
            {
                // run job without blocking the loop — fire-and-forget per job
                _ = Task.Run(() => HandleJobAsync(job, stoppingToken), stoppingToken);
            }
        }
    }

    private async Task HandleJobAsync(ToolJob job, CancellationToken cancellationToken)
    {
        // Create a new scope so DbContext and services are fresh for this background execution
        using var scope = _scopeFactory.CreateScope();
        var projectService = scope.ServiceProvider.GetRequiredService<IProjectService>();
        var executedToolService = scope.ServiceProvider.GetRequiredService<IExecutedToolService>();
        var executionId = job.ExecutionId.ToString();


        var project = await projectService.GetByName(job.Request.ProjectName);
        var executedTool = ProjectFactory.CreateExecutedToolFromExecutionRequest(toolId: Guid.Parse(job.ExecutionId), request: job.Request, project: project!, runnerId: null);
        await executedToolService.AddNew(executedTool);
        // await projectService.AddNewExecutedTool(executedTool);

        var process = processManager.CreateNewProcess(job.Request);

        // Wire events
        process.OutputDataReceived += async (s, e) =>
        {
            if (e.Data is not null)
            {
                // send to SignalR group
                await _hub.Clients.Group(executionId).SendAsync("ReceiveOutput", e.Data);
                WriteLine($"DATA: {e.Data}");
                var executedToolServiceI = scope.ServiceProvider.GetRequiredService<IExecutedToolService>();
                await executedToolServiceI.UpdateExecutedToolOutput(executedTool.Id.ToString(), e.Data);
            }
        };

        process.ErrorDataReceived += async (s, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                await _hub.Clients.Group(executionId).SendAsync("ReceiveError", e.Data);
                WriteLine($"ERROR: {e.Data}");
                // await MarkFailedAsync(projectService, executionId, e.Data);
            }
        };

        var exitTcs = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);

        process.Exited += (s, e) =>
        {
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
                // mark done
                executedTool.Status = ExecutionStatus.Done;
                executedTool.FinishedDate = DateTime.UtcNow;
                // await projectService.UpdateExecutedToolAsync(executedTool);

                await _hub.Clients.Group(executionId).SendAsync("StatusUpdate", ExecutionStatus.Done.ToString());
                await _hub.Clients.Group(executionId).SendAsync("Completed", new { executionId, exitCode });
            }
        }
        catch (OperationCanceledException)
        {
            // cancellation requested
            executedTool.Status = ExecutionStatus.Failed;
            executedTool.FinishedDate = DateTime.UtcNow;
            // await projectService.UpdateExecutedToolAsync(executedTool);
            await _hub.Clients.Group(executionId).SendAsync("StatusUpdate", ExecutionStatus.Failed.ToString());
        }
        catch (Exception ex)
        {
            // general failure
            executedTool.Status = ExecutionStatus.Failed;
            executedTool.FinishedDate = DateTime.UtcNow;
            // await projectService.UpdateExecutedToolAsync(executedTool);
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

    // helper: append output to DB safely
    // private static Task AppendOutputAsync(IProjectService projectService, string executionId, string output)
    // {
    //     // implement in your ProjectService: append text to the output column or write to separate table
    //     // keep DB operations async
    //     return projectService.AppendExecutionOutputAsync(executionId, output);
    // }

    // private static Task MarkFailedAsync(IProjectService projectService, string executionId, string error)
    // {
    //     return projectService.MarkExecutionFailedAsync(executionId, error);
    // }


}
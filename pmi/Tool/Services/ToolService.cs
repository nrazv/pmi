
using pmi.Tool.Models;
using pmi.Tool.Managers;
using pmi.Utilities;
using pmi.ExecutedTool.Models;
using pmi.Project.Service;

namespace pmi.Tool.Services;

public class ToolService : AsyncToolService
{

    private readonly ProcessManager processManager;
    private readonly IProjectService projectService;
    private readonly ObservableProcessResults processResults;

    public ToolService(IProjectService projectService, ObservableProcessResults processResults)
    {
        processManager = new ProcessManager();
        this.projectService = projectService;
        this.processResults = processResults;
    }



    public override void RunProcess(ToolExecutionRequest request)
    {
        var process = processManager.CreateNewProcess(request);
        string executedToolId = Guid.NewGuid().ToString();
        // createExecutedTool(request, executedToolId);

        try
        {
            process.OutputDataReceived += (obj, dataArgs) =>
            {
                if (dataArgs.Data is not null)
                {
                    WriteLine(dataArgs.Data);
                    string processOutput = dataArgs.Data;
                    processResults.Append(executedToolId, processOutput);
                    updateExecutedTool(executedToolId, processOutput);
                }
            };

            process.ErrorDataReceived += (obj, dataArgs) =>
            {
                if (!string.IsNullOrEmpty(dataArgs.Data))
                {
                    WriteLine($"Process Error: {dataArgs.Data}");
                    processResults.Remove(executedToolId);

                    var executedTool = projectService.GetExecutedTooById(executedToolId);
                    if (executedTool is not null)
                    {
                        executedTool.Status = ExecutionStatus.Failed;
                        executedTool.FinishedDated = DateTime.Now;
                        projectService.UpdateExecutedToo(executedTool);
                    }
                }
            };

            process.Exited += (obj, dataArgs) =>
            {
                WriteLine($"Process Exited");
                updateExecutedToolStatus(executedToolId, ExecutionStatus.Done);
            };

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
        }
        catch (Exception ex)
        {
            WriteLine($"Process Exception ERROR: {ex.Message}");
            processResults.Remove(executedToolId);
            updateExecutedToolStatus(executedToolId, ExecutionStatus.Failed);
        }
        finally
        {
            process.Dispose();
            Write("Process Disposed");
            updateExecutedToolStatus(executedToolId, ExecutionStatus.Done);
        }
    }



    public override List<ExecutedToolEntity> GetExecutedToolsByProjectName(string projectName)
    {
        return projectService.GetExecutedToolEntitiesByProjectName(projectName);
    }

    private void updateExecutedTool(string toolId, string newValue)
    {
        var executedTool = projectService.GetExecutedTooById(toolId);
        if (executedTool is not null)
        {
            executedTool.ExecutionResult = $"{executedTool.ExecutionResult}\n {newValue}";
            projectService.UpdateExecutedToo(executedTool);
        }
    }

    private void updateExecutedToolStatus(string toolId, ExecutionStatus? executionStatus)
    {
        var executedTool = projectService.GetExecutedTooById(toolId);
        if (executedTool is not null)
        {
            executedTool.Status = executionStatus;
            executedTool.FinishedDated = DateTime.Now;
            projectService.UpdateExecutedToo(executedTool);
        }
    }

    private async Task createExecutedTool(ToolExecutionRequest request, Guid executedToolId)
    {
        var project = await projectService.GetByName(request.ProjectName);
        var executedTool = ProjectFactory.CreateExecutedToolFromExecutionRequest(toolId: executedToolId, request: request, project: project!, runnerId: null);
        projectService.AddNewExecutedTool(executedTool);
    }
}

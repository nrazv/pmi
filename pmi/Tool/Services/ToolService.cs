using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using pmi.Project.Models;
using pmi.Project.Services;
using pmi.Tool.Mappers;
using pmi.Tool.Models;
using pmi.Tool.Runner;
using pmi.Utilities;

namespace pmi.Tool.Services;

public class ToolService : AsyncToolService
{

    private readonly ProcessManager processManager;
    private readonly IWebSocketService webSocketService;
    private readonly IProjectService projectService;
    private readonly ToolWSMapper toolWSMapper;

    public ToolService(IWebSocketService webSocketService, IProjectService projectService)
    {
        this.webSocketService = webSocketService;
        processManager = new ProcessManager(webSocketService, projectService);
        toolWSMapper = new ToolWSMapper();
        this.projectService = projectService;
    }


    public override string RunTool(ToolExecutionRequest toolExecution)
    {
        return processManager.RunTool(toolExecution);
    }

    public override async Task ExecuteToolViaWebSocket(HttpContext context)
    {
        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var request = await toolWSMapper.readRequestFromSocket(webSocket);

        if (request is null)
        {
            return;
        }

        setRequestId(request);
        webSocketService.RegisterClient(webSocket, request);
        var process = processManager.CreateNewProcess(request);
        string executedToolId = Guid.NewGuid().ToString();
        createExecutedTool(request, executedToolId);



        try
        {
            process.ErrorDataReceived += (s, e) => _ = processManager.HandleErrorDataReceived(e, webSocket);
            process.Exited += (s, e) => _ = processManager.HandleProcessExited(process, request);
            process.OutputDataReceived += async (s, e) => await HandleOutputAsync(e, webSocket, executedToolId);


            process.Start();
            setRunnerId(process.Id.ToString(), executedToolId);
            process.BeginOutputReadLine();
            process.WaitForExit();
        }
        catch (Exception ex)
        {
            byte[] errorBytes = Encoding.UTF8.GetBytes($"Exception: {ex.Message}");
            await webSocket.SendAsync(new ArraySegment<byte>(errorBytes), WebSocketMessageType.Text, true, CancellationToken.None);
            WriteLine(ex.Message);
            process.Dispose();
        }
        finally
        {
            var exitCode = process.ExitCode;
            process.Dispose();
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing connection", CancellationToken.None);
            var executedTool = projectService.GetExecutedTooById(executedToolId);
            if (executedTool is not null && exitCode == 0)
            {
                executedTool.Status = ExecutionStatus.Done;
                executedTool.FinishedDated = DateTime.Now;
                projectService.UpdateExecutedToo(executedTool);
            }

        }

    }

    private void setRequestId(ToolExecutionRequest request)
    {
        if (request.ClientId is null) request.ClientId = Guid.NewGuid().ToString();
    }

    public override List<ExecutedToolEntity> GetExecutedToolsByProjectName(string projectName)
    {
        return projectService.GetExecutedToolEntitiesByProjectName(projectName);
    }


    private async Task HandleOutputAsync(DataReceivedEventArgs e, WebSocket webSocket, string toolId)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            await processManager.HandleOutputAsync(e, webSocket);
            var executedTool = projectService.GetExecutedTooById(toolId);
            if (executedTool is not null)
            {
                executedTool.ExecutionResult = $"{executedTool.ExecutionResult}\n {e.Data}";
                projectService.UpdateExecutedToo(executedTool);
            }
        }
    }

    private void createExecutedTool(ToolExecutionRequest request, string executedToolId)
    {
        var project = projectService.GetByName(request.ProjectName);
        var executedTool = ProjectFactory.CreateExecutedToolFromExecutionRequest(toolId: executedToolId, request: request, project: project, runnerId: null);
        projectService.AddNewExecutedTool(executedTool);
    }

    private void setRunnerId(string runnerId, string executedToolId)
    {
        var executedTool = projectService.GetExecutedTooById(executedToolId);
        if (executedTool is not null) executedTool.RunnerId = runnerId;
        projectService.UpdateExecutedToo(executedTool);

    }
}

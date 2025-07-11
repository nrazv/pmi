using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using pmi.Tool.Mappers;
using pmi.Tool.Models;
using pmi.Tool.Runner;

namespace pmi.Tool.Services;

public class WebSocketService : IWebSocketService
{
    private readonly ToolWSMapper toolWSMapper;

    private readonly ProcessManager processManager;
    private readonly ObservableProcessResults processResults;



    public WebSocketService(ObservableProcessResults processResults)
    {
        toolWSMapper = new ToolWSMapper();
        this.processResults = processResults;
    }

    public async Task GetExecutedToolResult(HttpContext context)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var buffer = new byte[1024 * 4];
        string? listeningKey = null;

        // Subscribe to result updates
        void OnResultChanged(object? sender, ProcessResultChangedEventArgs e)
        {
            if (e.Key == listeningKey)
            {
                var message = Encoding.UTF8.GetBytes(e.NewValue ?? "");
                webSocket.SendAsync(
                    new ArraySegment<byte>(message, 0, message.Length),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None
                );
            }
        }

        processResults.ResultChanged += OnResultChanged;
        try
        {
            // Wait for client to send the key
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            listeningKey = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine($"Client is now listening to: {listeningKey}");

            // Keep socket open while client is connected
            while (!result.CloseStatus.HasValue)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
        finally
        {
            processResults.ResultChanged -= OnResultChanged;
            webSocket.Dispose();
        }

    }

    public async Task ExecuteToolViaWebSocket(HttpContext context)
    {
        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var request = await toolWSMapper.readRequestFromSocket(webSocket);

        if (request is null)
        {
            return;
        }

        var process = processManager.CreateNewProcess(request);
        string executedToolId = Guid.NewGuid().ToString();



        try
        {
            process.ErrorDataReceived += (s, e) => _ = processManager.HandleErrorDataReceived(e, webSocket);
            process.Exited += (s, e) => _ = processManager.HandleProcessExited(process, request);
            // process.OutputDataReceived += async (s, e) => await HandleOutputAsync(e, webSocket, executedToolId);
            process.Start();
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

        }

    }

    private async Task HandleOutputAsync(DataReceivedEventArgs e, WebSocket webSocket, string toolId)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            await processManager.HandleOutputAsync(e, webSocket);
            // var executedTool = projectService.GetExecutedTooById(toolId);
            // if (executedTool is not null)
            // {
            //     executedTool.ExecutionResult = $"{executedTool.ExecutionResult}\n {e.Data}";
            //     projectService.UpdateExecutedToo(executedTool);
            // }
        }
    }

}
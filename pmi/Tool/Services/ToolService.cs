using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using pmi.Tool.Models;
using pmi.Tool.Runner;

namespace pmi.Tool.Services;

public class ToolService : AsyncToolService
{
    private readonly ToolRunner _runner;

    public ToolService()
    {
        _runner = new ToolRunner();
    }

    public string RunTool(ToolExecutionRequest toolExecution)
    {
        return _runner.RunTool(toolExecution);
    }

    public override async Task ExecuteToolAsync(HttpContext httpContext, WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];

        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                ToolExecutionRequest toolExecutionRequest;

                try
                {
                    toolExecutionRequest = mapToToolExecutionRequest(buffer, result);
                    await _runner.ExecuteToolAsync(toolExecutionRequest, webSocket);
                }
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                }
            }
            else if (result.MessageType == WebSocketMessageType.Close)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
        }
    }

    private ToolExecutionRequest mapToToolExecutionRequest(byte[] buffer, WebSocketReceiveResult socketReceiveResult)
    {
        string messageJsonString = Encoding.UTF8.GetString(buffer, 0, socketReceiveResult.Count);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        ToolExecutionRequest? toolExecutionRequest = JsonSerializer.Deserialize<ToolExecutionRequest>(messageJsonString, options);
        if (toolExecutionRequest is null)
        {
            throw new ArgumentNullException(nameof(WebSocketReceiveResult), "Object sent in WebSocketReceiveResult can't be null");
        }
        return toolExecutionRequest;
    }
}

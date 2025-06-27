using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using pmi.Tool.Models;

namespace pmi.Tool.Mappers;

public class ToolWSMapper
{
    public ToolExecutionRequest mapToToolExecutionRequest(byte[] buffer, WebSocketReceiveResult socketReceiveResult)
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

    public async Task<ToolExecutionRequest> readRequestFromSocket(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        return mapToToolExecutionRequest(buffer, result);
    }
}
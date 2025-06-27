using System.Diagnostics;
using System.Net.WebSockets;
using pmi.Tool.Models;

namespace pmi.Tool.Services;

public interface IWebSocketService
{
    public void RegisterClient(WebSocket client, ToolExecutionRequest request);
    public void UnregisterClient(ToolExecutionRequest request);

}
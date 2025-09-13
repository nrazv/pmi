using System.Diagnostics;
using System.Net.WebSockets;
using pmi.Tool.Models;

namespace pmi.Tool.Services;

public interface IWebSocketService
{
    public Task GetExecutedToolResult(HttpContext context);
    public Task ExecuteToolViaWebSocket(HttpContext context);

}
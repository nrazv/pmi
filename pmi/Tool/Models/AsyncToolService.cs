using System.Net.WebSockets;
using pmi.Tool.Services;

namespace pmi.Tool.Models;

public abstract class AsyncToolService : IToolService
{
    public AsyncToolService() { }

    public virtual async Task ExecuteToolAsync(HttpContext httpContext, WebSocket webSocket)
    {
        throw new NotImplementedException();
    }

    public string RunTool(ToolExecutionRequest toolExecution)
    {
        throw new NotImplementedException();
    }
}
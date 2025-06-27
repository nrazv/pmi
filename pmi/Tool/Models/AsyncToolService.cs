using System.Net.WebSockets;
using pmi.Project.Models;
using pmi.Tool.Services;

namespace pmi.Tool.Models;

public abstract class AsyncToolService : IToolService
{
    public AsyncToolService() { }

    public virtual async Task ExecuteToolViaWebSocket(HttpContext context)
    {
        throw new NotImplementedException();
    }

    public virtual List<ExecutedToolEntity> GetExecutedToolsByProjectName(string projectName)
    {
        throw new NotImplementedException();
    }

    public virtual string RunTool(ToolExecutionRequest toolExecution)
    {
        throw new NotImplementedException();
    }
}
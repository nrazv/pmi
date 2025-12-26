using System.Threading.Channels;
using Microsoft.AspNetCore.Mvc;
using pmi.DataContext;
using pmi.ExecutedTool.Models;
using pmi.Tool.Models;
using pmi.Tool.Services;

namespace pmi.Tool;

[ApiController]
[Route("api/tool")]
public class ToolController : ControllerBase
{
    private IToolService _toolService;
    private ToolsDataJSON _toolsDataJSON;
    private readonly Channel<ToolJob> _channel;

    public ToolController(IToolService toolService, ToolsDataJSON toolsDataJSON, Channel<ToolJob> channel)
    {
        _channel = channel;
        _toolService = toolService;
        _toolsDataJSON = toolsDataJSON;
    }


    [HttpPost("execute", Name = "Run a tool")]
    public IActionResult StartTool(ToolExecutionRequest request)
    {
        //{"target":"scanme.nmap.org","tool":"nmap","arguments":"-sC -sV -vv","projectName":"scanme.org"}

        var id = Guid.NewGuid().ToString();
        var job = new ToolJob { ExecutionId = id, Request = request };

        // fire-and-forget enqueue
        _channel.Writer.TryWrite(job); // optional: check TryWrite and return 503 if buffer full

        // 202 Accepted with location to poll (optional)
        return Accepted(new { executionId = id });
    }


    [HttpGet("installed", Name = "Get installed tools list")]
    public List<InstalledTool> InstalledTools()
    {
        return _toolsDataJSON.InstalledTools;
    }

    [HttpGet("executed/{projectName}", Name = "Get executed tool for project")]
    public async Task<List<ExecutedToolEntity>> GetExecutedToolEntitiesForProject(string projectName)
    {
        return await _toolService.GetExecutedToolsByProjectName(projectName);
    }
}

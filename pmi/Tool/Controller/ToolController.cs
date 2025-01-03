using Microsoft.AspNetCore.Mvc;
using pmi.Tool.Models;
using pmi.Tool.Services;

namespace pmi.Tool;
[ApiController]
[Route("tool")]
public class ToolController : ControllerBase
{
    private IToolService _toolService;

    public ToolController(IToolService toolService)
    {
        _toolService = toolService;
    }

    [HttpPost(Name = "Run a tool")]
    public string StartTool(ToolExecutionRequest toolExecution)
    {
        return _toolService.RunTool(toolExecution);
    }
}

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
    public void StartTool(ToolExecutionRequest toolExecution)
    {
        _toolService.RunTool(toolExecution);
    }
}

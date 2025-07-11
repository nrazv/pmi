using Microsoft.AspNetCore.Mvc;
using pmi.DataContext;
using pmi.Project.Models;
using pmi.Tool.Models;

namespace pmi.Tool;

[ApiController]
[Route("api/tool")]
public class ToolController : ControllerBase
{
    private AsyncToolService _toolService;
    private ToolsDataJSON _toolsDataJSON;

    public ToolController(AsyncToolService toolService, ToolsDataJSON toolsDataJSON)
    {
        _toolService = toolService;
        _toolsDataJSON = toolsDataJSON;

    }


    [HttpPost("execute", Name = "Run a tool")]
    public void StartTool(ToolExecutionRequest toolExecution)
    {
        _toolService.RunProcess(toolExecution);
    }


    [HttpGet("installed", Name = "Get installed tools list")]
    public List<InstalledTool> InstalledTools()
    {
        return _toolsDataJSON.InstalledTools;
    }

    [HttpGet("executed/{projectName}", Name = "Get executed tool for project")]
    public List<ExecutedToolEntity> GetExecutedToolEntitiesForProject(string projectName)
    {
        return _toolService.GetExecutedToolsByProjectName(projectName);
    }
}

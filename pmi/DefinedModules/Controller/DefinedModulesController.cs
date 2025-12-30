using Microsoft.AspNetCore.Mvc;
using pmi.DefinedModules.Models;
using pmi.DefinedModules.Services;
using pmi.ExecutedModule.Service;
using pmi.Modules.Models;

namespace pmi.DefinedModules.Controller;


[ApiController]
[Route("api/definedmodules")]

public class DefinedModulesController : ControllerBase
{
    private readonly IDefinedModuleService service;
    private readonly IExecutedModuleService _executedModuleService;

    public DefinedModulesController(IDefinedModuleService moduleService, IExecutedModuleService executedModuleService)
    {
        service = moduleService;
        _executedModuleService = executedModuleService;
    }


    [HttpPost("execute", Name = "Execute module")]
    public async Task<IActionResult> ExecuteModule(ModuleExecutionRequest request)
    {
        await service.ExecuteModuleAsync(request);
        return Ok();
    }

    [HttpGet("executed/{projectName}", Name = "Get executed modules by project name")]
    public async Task<List<ExecutedModuleEntity>> GetExecutedModule(string projectName)
    {
        var response = await _executedModuleService.GetExecutedModulesByProjectName(projectName);

        return response;
    }
}

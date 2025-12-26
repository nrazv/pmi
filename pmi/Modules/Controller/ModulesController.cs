using Microsoft.AspNetCore.Mvc;
using pmi.Modules.Dto;
using pmi.Modules.Models;
using pmi.Modules.Service;

namespace pmi.Modules.Controller;


[ApiController]
[Route("api/modules")]
public class ModulesController : ControllerBase
{

    private readonly IModuleService service;

    public ModulesController(IModuleService service)
    {
        this.service = service;
    }

    [HttpPost("create", Name = "Create new module")]
    [ProducesResponseType<ModuleEntity>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateModule(CreateModuleDto dto)
    {
        var result = await service.NewModule(dto);

        if (result.Success is false)
        {
            return BadRequest(new { message = result.ErrorMessage });
        }
        else
        {
            return CreatedAtAction(nameof(CreateModule), result);
        }

    }

    [HttpGet("all", Name = "Get all modules")]
    public async Task<List<ModuleEntity>> GetAll()
    {
        return await service.GetAll();
    }

    [HttpGet("{name}", Name = "Get module by name")]
    [ProducesResponseType<ModuleEntity>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByName(string name)
    {
        var result = await service.GetByName(name);

        if (result is not ModuleEntity)
        {
            return NotFound(new { message = $"Module with {name} not found" });
        }
        else
        {

            return Ok(result);
        }
    }

}
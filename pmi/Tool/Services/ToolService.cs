
using pmi.ExecutedTool.Models;
using pmi.ExecutedTool.Service;

namespace pmi.Tool.Services;

public class ToolService : IToolService
{


    private readonly IExecutedToolService _executedToolService;


    public ToolService(IExecutedToolService executedToolService)
    {
        _executedToolService = executedToolService;
    }

    public async Task<List<ExecutedToolEntity>> GetExecutedToolsByProjectName(string projectName)
    {
        return await _executedToolService.GetAllByProjectName(projectName);
    }
}

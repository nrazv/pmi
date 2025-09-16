
using pmi.Tool.Managers;
using pmi.ExecutedTool.Models;
using pmi.Project.Service;
using pmi.ExecutedTool.Service;
using System.Threading.Tasks;

namespace pmi.Tool.Services;

public class ToolService : IToolService
{

    private readonly IProjectService projectService;
    private readonly IExecutedToolService _executedToolService;


    public ToolService(IProjectService projectService, IExecutedToolService executedToolService)
    {
        this.projectService = projectService;
        _executedToolService = executedToolService;
    }

    public async Task<List<ExecutedToolEntity>> GetExecutedToolsByProjectName(string projectName)
    {
        return await _executedToolService.GetAllByProjectName(projectName);
    }
}

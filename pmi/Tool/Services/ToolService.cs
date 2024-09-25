using pmi.Tool.Models;
using pmi.Tool.Runner;

namespace pmi.Tool.Services;

public class ToolService : IToolService
{
    private readonly ToolRunner _runner;

    public ToolService()
    {
        _runner = new ToolRunner();
    }

    public void RunTool(ToolExecutionRequest toolExecution)
    {
        _runner.RunTool(toolExecution);
    }
}

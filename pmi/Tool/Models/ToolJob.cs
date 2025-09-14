namespace pmi.Tool.Models;

public class ToolJob
{
    public string ExecutionId { get; init; } = default!;
    public ToolExecutionRequest Request { get; init; } = default!;
}
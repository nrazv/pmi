namespace pmi.Tool.Models;

public class ToolExecutionRequest
{
    public required string Target { get; set; }
    public required string Tool { get; set; }
    public required string Arguments { get; set; }
    public required string ProjectName { get; set; }
    public string? ClientId { get; set; }

    public override string ToString()
    {
        return $"ToolExecutionRequest: Target={Target}, Tool={Tool}, Arguments={Arguments}, ProjectName={ProjectName}, ClientId={ClientId ?? "None"}";
    }
}

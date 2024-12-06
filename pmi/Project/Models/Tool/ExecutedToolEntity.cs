using System.ComponentModel.DataAnnotations;

namespace pmi.Project.Models;

public class ExecutedToolEntity
{
    [Key]
    public string Id { get; init; } = null!;
    public ExecutionStatus status { get; init; }
    public string Name { get; init; } = null!;
    public string? ExecutionResult { get; init; } = string.Empty;
    public DateTime ExecutedDated { get; init; }

}
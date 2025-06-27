using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pmi.Project.Models;


[Table("ExecutedTools")]
public class ExecutedToolEntity
{
    [Key]
    public required string Id { get; init; }
    public required string ProjectId { get; set; }
    [JsonIgnore]
    public ProjectEntity Project { get; set; }
    public required string Name { get; init; }
    public required string ToolArguments { get; set; }
    public required string Target { get; set; }
    public string? RunnerId { get; set; }
    public string? ClientId { get; set; }
    public ExecutionStatus? Status { get; set; }
    public string? ExecutionResult { get; set; } = string.Empty;
    public DateTime? ExecutedDated { get; set; }
    public DateTime? FinishedDated { get; set; }

}
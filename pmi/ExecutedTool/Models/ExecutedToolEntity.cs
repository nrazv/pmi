using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using pmi.Project.Models;

namespace pmi.ExecutedTool.Models;


[Table("ExecutedTools")]
public class ExecutedToolEntity
{
    [Key]
    public required Guid Id { get; init; }
    public required Guid ProjectId { get; set; }
    [JsonIgnore]
    public ProjectEntity Project { get; set; } = null!;
    public required string Name { get; init; }
    public required string ToolArguments { get; set; }
    public required string Target { get; set; }
    public string? RunnerId { get; set; }
    public string? ClientId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ExecutionStatus? Status { get; set; } = ExecutionStatus.NotStarted;
    public string? ExecutionResult { get; set; } = string.Empty;
    public DateTime? ExecutedDated { get; set; }
    public DateTime? FinishedDated { get; set; }

}
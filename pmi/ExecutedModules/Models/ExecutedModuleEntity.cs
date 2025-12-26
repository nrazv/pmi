using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using pmi.ExecutedTool.Models;
using pmi.Project.Models;

namespace pmi.Modules.Models;

[Table("ExecutedModules")]
public class ExecutedModuleEntity
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Target { get; set; }

    public Guid ProjectId { get; set; }
    [JsonIgnore]
    public ProjectEntity Project { get; set; } = null!;

    public List<ExecutedToolEntity> ExecutedTools { get; set; } = new();
}
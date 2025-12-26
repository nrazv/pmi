using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using pmi.Modules.Models;

namespace pmi.Tool.Models;

public class ExecutableTool
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Arguments { get; set; }

    public Guid ModuleEntityId { get; set; }

    [JsonIgnore]
    public ModuleEntity ModuleEntity { get; set; } = null!;
}